using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibox.Shared.Library.Setting
{
	public class MiniboxSettings
	{
		public AuthenticationSettings AuthenticationSettings { get; set; } = new AuthenticationSettings();
		public JwtSettings JwtSettings { get; set; } = new JwtSettings();
		public RetrySettings RetrySettings { get; set; } = new RetrySettings();
		public DbContextSettings DbContextSettings { get; set; } = new DbContextSettings();
		public SmtpSettings SmtpSettings { get; set; } = new SmtpSettings();
		public ExternalAthenticationSettings FacebookAuthenticationSettings { get; set; } = new ExternalAthenticationSettings();
		public ExternalAthenticationSettings GoogleAuthenticationSettings { get; set; } = new ExternalAthenticationSettings();
	}

	public class AuthenticationSettings
	{
		public int MaxActivationFailedCount { get; set; } = 0;
		public int MaxAccessFailedCount { get; set; } = 0;
		public int NumberOfDaysLocked { get; set; } = 0;
	}

	public class JwtSettings
	{
		public string SecretKey { get; set; } = string.Empty;
		public string ValidIssuer { get; set; } = string.Empty;
		public string ValidAudience { get; set; } = string.Empty;
		public int TokenValidityInMinutes { get; set; } = 0;
		public int AccessTokenValidityInDays { get; set; } = 0;
		public int RefreshTokenValidityInDays { get; set; } = 0;
	}

	public class RetrySettings
	{
		public int RetryMaxAttemptCount { get; set; } = 0;
		public int RetryIntervalInSeconds { get; set; } = 0;
	}

	public class DbContextSettings
	{
		public int BatchSize { get; set; } = 0;
		public int CmdTimeOutInMiliseconds { get; set; } = 0;
	}

	public class SmtpSettings
	{
		public string User { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string Host { get; set; } = string.Empty;
		public int Port { get; set; } = 0;
	}

	public class ExternalAthenticationSettings
	{
		public string ClientId { get; set; } = string.Empty;
		public string ClientSecret { get; set; } = string.Empty;
	}
}
