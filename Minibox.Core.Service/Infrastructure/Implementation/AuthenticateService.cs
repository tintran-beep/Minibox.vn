using AutoMapper;
using Azure;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Minibox.Core.Data.Database.Main;
using Minibox.Core.Data.Database.Main.Entity.Auth;
using Minibox.Core.Data.Infrastructure.Interface;
using Minibox.Core.Service.Infrastructure.Interface;
using Minibox.Shared.Library.Const;
using Minibox.Shared.Library.Enum;
using Minibox.Shared.Library.Extension;
using Minibox.Shared.Library.Setting;
using Minibox.Shared.Model.ViewModel;
using Minibox.Shared.Model.ViewModel.BaseVM;
using Minibox.Shared.Module.Logging.Infrastructure.Interface;
using OtpNet;
using QRCoder;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Minibox.Core.Service.Infrastructure.Implementation
{
	internal class AuthenticateService(
		IMapper mapper,
		IMiniboxLogger logger,
		IOptions<MiniboxSettings> appSettings,
		IUnitOfWork<MainDbContext> mainUnitOfWork)
		: BaseService(mapper, logger, appSettings, mainUnitOfWork), IAuthenticateService
	{
		/// <inheritdoc/>
		public async Task<ResponseVM<UserSignedInVM>> SignUpAsync(UserSignUpVM model)
		{
			using var transaction = await _mainUnitOfWork.BeginTransactionAsync();
			try
			{
				var existingUser = await _mainUnitOfWork.Repository<User>()
														.Where(x => x.Username == model.Username)
														.FirstOrDefaultAsync();
				if (existingUser != null)
				{
					return ResponseVM<UserSignedInVM>.Failure("Username already exists.");
				}

				var user = new User
				{
					Username = model.Username,
					NormalizedUsername = model.Username.ToLower(),
					Fullname = model.Fullname,
					NormalizedFullname = model.Fullname.ToLower(),
					PasswordHash = HashPassword(model.Password),
					Status = (int)MiniboxEnums.UserStatus.Active,
					TwoFactorEnabled = true
				};

				await _mainUnitOfWork.Repository<User>().InsertAsync(user);
				await _mainUnitOfWork.SaveChangesAsync(isPartOfTransaction: true);

				//Add user to Roles
				var adminRole = await _mainUnitOfWork.Repository<Role>().Where(x => x.Name == MiniboxConstants.Role.Admin).FirstOrDefaultAsync();
				if (adminRole == null)
				{
					await _mainUnitOfWork.RollbackTransactionAsync();
					return ResponseVM<UserSignedInVM>.Failure($"Default role not found!");
				}

				var userRole = new UserRole(user.Id, adminRole.Id);
				await _mainUnitOfWork.Repository<UserRole>().InsertAsync(userRole);
				await _mainUnitOfWork.SaveChangesAsync(isPartOfTransaction: true);
				await _mainUnitOfWork.CommitTransactionAsync();

				var accessToken = GenerateToken(user, [adminRole], []);

				return ResponseVM<UserSignedInVM>.Success(new UserSignedInVM()
				{
					Id = user.Id,
					Username = user.Username,
					Fullname = user.Fullname,
					AccessToken = accessToken,
				});
			}
			catch (Exception ex)
			{
				await _mainUnitOfWork.RollbackTransactionAsync();
				return ResponseVM<UserSignedInVM>.Failure($"Sign up failed: {ex.Message}");
			}
		}

		/// <inheritdoc/>
		public Task<ResponseVM<UserSignedInVM>> SignInAsync(UserSignInVM model)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc/>
		public async Task<ResponseVM<UserSignedInVM>> AdminSignInAsync(UserSignInVM model)
		{
			using var transaction = await _mainUnitOfWork.BeginTransactionAsync();
			try
			{
				var user = await _mainUnitOfWork.Repository<User>()
												.Where(x => x.Username.ToLower().Trim() == model.Username.ToLower().Trim())
												.FirstOrDefaultAsync() ?? throw new Exception($"User {model.Username} not found!");

				if (!VerifyPassword(model.Password, user.PasswordHash))
				{
					await HandleFailedLoginAttempt(user);
					return ResponseVM<UserSignedInVM>.Failure("Password is incorrect!");
				}

				switch ((MiniboxEnums.UserStatus)user.Status)
				{
					case MiniboxEnums.UserStatus.Active:
						// Check Two-Factor Authentication (2FA)
						var response = await HandleTwoFactorAuthentication(user);

						await HandleActiveUser(user);

						return response;

					case MiniboxEnums.UserStatus.InActive:
						if (user.LockoutEndDate_Utc != null && DateTime.UtcNow < user.LockoutEndDate_Utc)
							throw new Exception($"{user.LockedReason}");

						// Unlock the account
						await HandleActiveUser(user);

						goto case MiniboxEnums.UserStatus.Active;

					case MiniboxEnums.UserStatus.New:
						// Activate the account
						await HandleActiveUser(user);

						goto case MiniboxEnums.UserStatus.Active;

					default:
						throw new Exception($"User {user.Username} is not in a valid status!");
				}
			}
			catch (Exception ex)
			{
				await _mainUnitOfWork.RollbackTransactionAsync();
				_logger.LogError($"SignIn for user {model.Username} failed!", ex);
				return ResponseVM<UserSignedInVM>.Failure($"SignIn for user {model.Username} failed: {ex.Message}");
			}
		}

		/// <inheritdoc/>
		public async Task<ResponseVM<UserGeneratedOtpVM>> GenerateOtpAsync(UserGenerateOtpVM model)
		{
			using var transaction = await _mainUnitOfWork.BeginTransactionAsync();
			try
			{
				var user = await _mainUnitOfWork.Repository<User>()
												.Where(x => x.Username.ToLower().Trim() == model.Username.ToLower().Trim())
												.FirstOrDefaultAsync() ?? throw new Exception($"User:{model.Username} not found!");

				var key = KeyGeneration.GenerateRandomKey(20);
				var secretKey = Base32Encoding.ToString(key);
				var otpAuthUrl = $"otpauth://totp/{MiniboxConstants.AppName}:{model.Username}?secret={secretKey}&issuer={MiniboxConstants.AppName}&algorithm=SHA1&digits=6&period=30";

				var qrCodeBase64 = await Task.Run(() =>
				{
					var qrGenerator = new QRCodeGenerator();
					var qrCodeData = qrGenerator.CreateQrCode(otpAuthUrl, QRCodeGenerator.ECCLevel.Q);
					var qrCode = new PngByteQRCode(qrCodeData);
					var qrCodeBytes = qrCode.GetGraphic(20);
					return Convert.ToBase64String(qrCodeBytes);
				});

				user.SecretKey = secretKey;

				_mainUnitOfWork.Repository<User>().Update(user);
				await _mainUnitOfWork.SaveChangesAsync(isPartOfTransaction: true);
				await _mainUnitOfWork.CommitTransactionAsync();

				return ResponseVM<UserGeneratedOtpVM>.Success(new UserGeneratedOtpVM()
				{
					Username = user.Username,
					SecretKey = user.SecretKey,
					QrCodeUrl = $"data:image/png;base64,{qrCodeBase64}"
				});
			}
			catch (Exception ex)
			{
				await _mainUnitOfWork.RollbackTransactionAsync();
				_logger.LogError($"Generate OTP Secret for user {model.Username} failed!", ex);
				return ResponseVM<UserGeneratedOtpVM>.Failure($"Generate OTP Secret for user {model.Username} failed: {ex.Message}");
			}
		}

		/// <inheritdoc/>
		public async Task<ResponseVM<UserVerifiedOtpVM>> VerifyOtpAsync(UserVerifyOtpVM model)
		{
			try
			{
				var user = await _mainUnitOfWork.Repository<User>()
												.Where(x => x.Username.ToLower().Trim() == model.Username.ToLower().Trim())
												.FirstOrDefaultAsync() ?? throw new Exception($"User:{model.Username} not found!");

				if (string.IsNullOrWhiteSpace(user.SecretKey))
					throw new Exception($"User {model.Username} does not have SecretKey");
								
				var totp = new Totp(Base32Encoding.ToBytes(user.SecretKey));
				var isValid = totp.VerifyTotp(model.OtpCode, out long _);

				if (!isValid)
				{
					if (user.VerifyFailedCount >= _appSettings.AuthenticationSettings.MaxActivationFailedCount)
					{
						user.VerifyFailedCount = 0;
						user.Status = (int)MiniboxEnums.UserStatus.InActive;
						user.LockoutEndDate_Utc = DateTime.UtcNow.AddDays(_appSettings.AuthenticationSettings.NumberOfDaysLocked);

						var lockedEndDate = MiniboxExtensions.DateTimeHelper.ConvertFromUtc(user.LockoutEndDate_Utc, user.TimeZoneId)?.ToString("dd/mm/yyyy HH:mm:ss") ?? string.Empty;

						user.LockedReason = $"User is locked until {lockedEndDate} due to failed verify OTP attempts!";

						_mainUnitOfWork.Repository<User>().Update(user);
						await _mainUnitOfWork.SaveChangesAsync();

						return ResponseVM<UserVerifiedOtpVM>.Failure(new UserVerifiedOtpVM()
						{
							Status = user.Status
						}, user.LockedReason);
					}
					else
					{
						user.VerifyFailedCount++;
						_mainUnitOfWork.Repository<User>().Update(user);
						await _mainUnitOfWork.SaveChangesAsync();

						throw new Exception($"OTP {model.OtpCode} incorrect!");
					}
				}				

				var userRoles = await GetAllRolesByUserId(user.Id);
				var userClaims = await GetAllClaimsByUserId(user.Id);
				var accessToken = GenerateToken(user, userRoles, userClaims);

				return ResponseVM<UserVerifiedOtpVM>.Success(new UserVerifiedOtpVM()
				{
					Id = user.Id,
					Status = user.Status,
					Username = user.Username,
					Fullname = user.Fullname,
					AccessToken = accessToken,
					Roles = _mapper.Map<List<RoleVM>>(userRoles),
					Claims = _mapper.Map<List<ClaimVM>>(userClaims),
				});
			}
			catch (Exception ex)
			{
				_logger.LogError($"Verify OTP for user {model.Username} failed!", ex);
				return ResponseVM<UserVerifiedOtpVM>.Failure($"Verify OTP for user {model.Username} failed: {ex.Message}");
			}
		}

		/// <inheritdoc/>
		public Task SignOutAsync(Guid userId)
		{
			throw new NotImplementedException();
		}

		#region private
		private async Task<List<Role>> GetAllRolesByUserId(Guid userId)
		{
			var userRoles = await _mainUnitOfWork.Repository<UserRole>().Where(ur => ur.UserId == userId)
												 .Join(_mainUnitOfWork.Repository<Role>().Query(), ur => ur.RoleId, r => r.Id, (ur, r) => r)
												 .ToListAsync() ?? [];
			return userRoles;
		}

		private async Task<List<Claim>> GetAllClaimsByUserId(Guid userId)
		{
			var userClaims = await _mainUnitOfWork.Repository<UserClaim>()
								.Where(uc => uc.UserId == userId)
								.Join(_mainUnitOfWork.Repository<Claim>().Query(), uc => uc.ClaimId, c => c.Id, (uc, c) => c)
								.Union(
									_mainUnitOfWork.Repository<UserRole>()
									.Where(ur => ur.UserId == userId)
									.Join(_mainUnitOfWork.Repository<RoleClaim>().Query(), ur => ur.RoleId, rc => rc.RoleId, (ur, rc) => rc)
									.Join(_mainUnitOfWork.Repository<Claim>().Query(), rc => rc.ClaimId, c => c.Id, (rc, c) => c)
								)
								.Distinct()
								.ToListAsync() ?? [];
			return userClaims;
		}

		private async Task HandleFailedLoginAttempt(User user)
		{
			if (user.Status == (int)MiniboxEnums.UserStatus.Active || user.Status == (int)MiniboxEnums.UserStatus.New)
			{
				if (user.AccessFailedCount >= _appSettings.AuthenticationSettings.MaxAccessFailedCount)
				{
					user.AccessFailedCount = 0;
					user.Status = (int)MiniboxEnums.UserStatus.InActive;
					user.LockoutEndDate_Utc = DateTime.UtcNow.AddDays(_appSettings.AuthenticationSettings.NumberOfDaysLocked);

					var lockedEndDate = MiniboxExtensions.DateTimeHelper.ConvertFromUtc(user.LockoutEndDate_Utc, user.TimeZoneId)?.ToString("dd/mm/yyyy HH:mm:ss") ?? string.Empty;

					user.LockedReason = $"User is locked until {lockedEndDate} due to failed login attempts!";
				}
				else
				{
					user.AccessFailedCount++;
				}

				_mainUnitOfWork.Repository<User>().Update(user);
				await _mainUnitOfWork.SaveChangesAsync();
			}
		}

		private async Task<ResponseVM<UserSignedInVM>> HandleTwoFactorAuthentication(User user)
		{
			if (user.TwoFactorEnabled)
			{
				return ResponseVM<UserSignedInVM>.Success(new UserSignedInVM()
				{
					Id = user.Id,
					Username = user.Username,
					Fullname = user.Fullname,
					TwoFactorEnabled = true,
					SecretKey = user.SecretKey,
				});
			}

			// If 2FA is disabled, generate an AccessToken and return user details
			var userRoles = await GetAllRolesByUserId(user.Id);
			var userClaims = await GetAllClaimsByUserId(user.Id);
			var accessToken = GenerateToken(user, userRoles, userClaims);

			return ResponseVM<UserSignedInVM>.Success(new UserSignedInVM()
			{
				Id = user.Id,
				Username = user.Username,
				Fullname = user.Fullname,
				TwoFactorEnabled = false,
				AccessToken = accessToken,
				Roles = _mapper.Map<List<RoleVM>>(userRoles),
				Claims = _mapper.Map<List<ClaimVM>>(userClaims),
			});
		}

		private async Task HandleActiveUser(User user)
		{
			user.Status = (int)MiniboxEnums.UserStatus.Active;
			user.AccessFailedCount = 0;
			user.VerifyFailedCount = 0;
			user.LockoutEndDate_Utc = null;
			user.LockedReason = string.Empty;

			_mainUnitOfWork.Repository<User>().Update(user);
			await _mainUnitOfWork.SaveChangesAsync(isPartOfTransaction: true);
			await _mainUnitOfWork.CommitTransactionAsync();
		}

		private string GenerateToken(User user, List<Role> roles, List<Claim> claims)
		{
			var jwtSettings = _appSettings.JwtSettings;
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var tokenClaims = new List<System.Security.Claims.Claim>
			{
				new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
				new(JwtRegisteredClaimNames.UniqueName, user.Username),
				new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			tokenClaims.AddRange(roles.Select(role => new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, role.Name)));

			tokenClaims.AddRange(claims.Select(claim => new System.Security.Claims.Claim(claim.Type, claim.Value)));

			var token = new JwtSecurityToken(
				issuer: jwtSettings.ValidIssuer,
				audience: jwtSettings.ValidAudience,
				claims: tokenClaims,
				expires: DateTime.UtcNow.AddMinutes(jwtSettings.TokenValidityInMinutes),
				signingCredentials: credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		private static string HashPassword(string password)
		{
			return BCrypt.Net.BCrypt.HashPassword(password);
		}

		private static bool VerifyPassword(string password, string hashedPassword)
		{
			return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
		}
		#endregion
	}
}
