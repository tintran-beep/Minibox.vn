using Minibox.Shared.Library.Extension;

namespace Minibox.Core.Data.Database.Main.Entity.Auth
{
	public class User : BaseEntity
	{
		public User() 
		{
			Id = MiniboxExtensions.SequentialGuidGenerator.Generate();
		}

		public Guid Id { get; set; }

		public string Fullname { get; set; } = string.Empty;

		public string NormalizedFullname { get; set; } = string.Empty;

		public string Username { get; set; } = string.Empty;

		public string NormalizedUsername { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;

		public string NormalizedEmail { get; set; } = string.Empty;

		public bool EmailConfirmed { get; set; } = false;

		public string PasswordHash { get; set; } = string.Empty;

		public string SecurityStamp { get; set; } = string.Empty;

		public string ConcurrencyStamp { get; set; } = string.Empty;

		public string PhoneNumber { get; set; } = string.Empty;

		public string PhoneNumberConfirmed { get; set; } = string.Empty;

		public bool TwoFactorEnabled { get; set; } = false;

		public int AccessFailedCount { get; set; } = 0;

		public string AvatarUrl { get; set; } = string.Empty;

		public DateTime? DateOfBirth { get; set; }

		public DateTime? LockoutEndDate_Utc { get; set; }

		public DateTime CreatedDate_Utc { get; set; } = DateTime.UtcNow;

		public DateTime ModifiedDate_Utc { get; set; } = DateTime.UtcNow;

		public virtual ICollection<UserRole> UserRoles { get; set; } = [];
		public virtual ICollection<UserClaim> UserClaims { get; set; } = [];
		public virtual ICollection<UserLogin> UserLogins { get; set; } = [];
		public virtual ICollection<UserToken> UserTokens { get; set; } = [];
	}
}
