using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibox.Shared.Model.ViewModel
{
	public class UserSignInVM
	{
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public bool RememberMe { get; set; } = false;
	}
	
	public class UserSignedInVM
	{
		public Guid Id { get; set; }

		public string Username { get; set; } = string.Empty;

		public string Fullname { get; set; } = string.Empty;

		public bool TwoFactorEnabled { get; set; } = false;

		public string SecretKey { get; set; } = string.Empty;

		public string AccessToken { get; set; } = string.Empty;

		public ICollection<RoleVM> Roles { get; set; } = [];
		public ICollection<ClaimVM> Claims { get; set; } = [];
	}

	public class UserGenerateOtpVM
	{
		public string Username { get; set; } = string.Empty;
	}

	public class UserGeneratedOtpVM
	{
		public string Username { get; set; } = string.Empty;
		public string SecretKey { get; set; } = string.Empty;
		public string QrCodeUrl { get; set; } = string.Empty;
	}

	public class UserVerifyOtpVM
	{
		public string Username { get; set; } = string.Empty;
		public string SecretKey { get; set; } = string.Empty;
		public string OtpCode { get; set; } = string.Empty;
	}

	public class UserVerifiedOtpVM : UserSignedInVM
	{
		public int Status { get; set; }
	}

	public class UserSignUpVM
	{
		public string Username { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string Fullname { get; set; } = string.Empty;
	}
}
