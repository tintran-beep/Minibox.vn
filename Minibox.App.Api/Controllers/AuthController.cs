using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minibox.Core.Service.Infrastructure.Interface;
using Minibox.Shared.Model.ViewModel;

namespace Minibox.App.Api.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	public class AuthController(
		IAuthenticateService authenticateService) : ControllerBase
	{
		private readonly IAuthenticateService _authenticateService = authenticateService;

		/// <summary>
		/// Register a new user and return a signed-in response.
		/// </summary>
		/// <param name="model">User registration details.</param>
		/// <returns>Signed-in user response.</returns>
		[HttpPost("signup")]
		public async Task<IActionResult> SignUp([FromBody] UserSignUpVM model)
		{
			var response = await _authenticateService.SignUpAsync(model);
			if (!response.IsSuccess) 
				return BadRequest(response);
			return Ok(response);
		}

		[HttpPost("signin")]
		public async Task<IActionResult> SignIn([FromBody] UserSignInVM model)
		{
			var response = await _authenticateService.SignInAsync(model);
			if (!response.IsSuccess) return Unauthorized(response);
			return Ok(response);
		}

		/// <summary>
		/// Sign in as an admin.
		/// </summary>
		/// <param name="model">Admin credentials.</param>
		/// <returns>Signed-in user response.</returns>
		[HttpPost("admin/signin")]
		public async Task<IActionResult> AdminSignIn([FromBody] UserSignInVM model)
		{
			var response = await _authenticateService.AdminSignInAsync(model);
			if (!response.IsSuccess) 
				return Unauthorized(response);
			return Ok(response);
		}

		/// <summary>
		/// Registers a Two-Factor Authentication (2FA) secret for an admin user.
		/// </summary>
		/// <param name="model">The request containing the username for 2FA registration.</param>
		/// <returns>
		/// Returns an HTTP 200 response with the generated OTP secret and QR code if successful.  
		/// Returns an HTTP 401 (Unauthorized) if the registration fails.
		/// </returns>
		[HttpPost("admin/register-2fa")]
		public async Task<IActionResult> AdminRegister2FAAsync([FromBody] UserGenerateOtpVM model)
		{
			var response = await _authenticateService.GenerateOtpAsync(model);
			if (!response.IsSuccess) 
				return Unauthorized(response);
			return Ok(response);
		}


		/// <summary>
		/// Sign out a user.
		/// </summary>
		/// <param name="userId">User ID to sign out.</param>
		/// <returns>No content.</returns>
		[Authorize]
		[HttpPost("signout")]
		public async Task<IActionResult> SignOut([FromBody] Guid userId)
		{
			await _authenticateService.SignOutAsync(userId);
			return NoContent();
		}
	}
}
