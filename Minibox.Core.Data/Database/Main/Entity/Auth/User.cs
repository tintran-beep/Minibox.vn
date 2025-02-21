using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Minibox.Shared.Library.Const;
using Minibox.Shared.Library.Enum;
using Minibox.Shared.Library.Extension;
using Minibox.Core.Data.Database.Main.Entity.Default;

namespace Minibox.Core.Data.Database.Main.Entity.Auth
{
	public class User : BaseEntity
	{
		public User() 
		{
			Id = MiniboxExtensions.SequentialGuidGenerator.Generate();
			Status = (int)MiniboxEnums.UserStatus.New;
		}

		public Guid Id { get; set; }
		public string Username { get; set; } = string.Empty;

		public string NormalizedUsername { get; set; } = string.Empty;

		public string Fullname { get; set; } = string.Empty;

		public string NormalizedFullname { get; set; } = string.Empty;

		public string Email { get; set; } = string.Empty;

		public string NormalizedEmail { get; set; } = string.Empty;

		public bool EmailConfirmed { get; set; } = false;

		public string PhoneNumber { get; set; } = string.Empty;

		public bool PhoneNumberConfirmed { get; set; } = false;

		public string PasswordHash { get; set; } = string.Empty;

		public string SecurityStamp { get; set; } = string.Empty;

		public string ConcurrencyStamp { get; set; } = string.Empty;

		public bool TwoFactorEnabled { get; set; } = false;

		public int? AccessFailedCount { get; set; }

		public int Status { get; set; }

		public Guid? AvatarId { get; set; }

		public DateTime? DateOfBirth { get; set; }

		public DateTime? LockoutEndDate_Utc { get; set; }

		public DateTime CreatedDate_Utc { get; set; } = DateTime.UtcNow;

		public DateTime ModifiedDate_Utc { get; set; } = DateTime.UtcNow;

		public virtual Media? Avartar { get; set; }
		public virtual ICollection<UserRole> UserRoles { get; set; } = [];
		public virtual ICollection<UserClaim> UserClaims { get; set; } = [];
		public virtual ICollection<UserLogin> UserLogins { get; set; } = [];
		public virtual ICollection<UserToken> UserTokens { get; set; } = [];
	}

	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{
			builder.ToTable(nameof(User), MiniboxConstants.DbSchema.Authentication);

			builder.HasKey(x => x.Id);

			builder.Property(x => x.Username).HasMaxLength(100).IsRequired();
			builder.Property(x => x.NormalizedUsername).HasMaxLength(100).IsRequired();

			builder.Property(x => x.Fullname).HasMaxLength(100).IsRequired();
			builder.Property(x => x.NormalizedFullname).HasMaxLength(100).IsRequired();

			builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
			builder.Property(x => x.NormalizedEmail).HasMaxLength(100).IsRequired();
			builder.Property(x => x.EmailConfirmed).HasDefaultValue(false).IsRequired();

			builder.Property(x => x.PhoneNumber).HasMaxLength(15).IsRequired();
			builder.Property(x => x.PhoneNumberConfirmed).HasDefaultValue(false).IsRequired();

			builder.Property(x => x.PasswordHash).HasMaxLength(int.MaxValue).IsRequired();
			builder.Property(x => x.SecurityStamp).HasMaxLength(int.MaxValue).IsRequired();
			builder.Property(x => x.ConcurrencyStamp).HasMaxLength(int.MaxValue).IsRequired();

			builder.Property(x => x.TwoFactorEnabled).HasDefaultValue(false).IsRequired();
			builder.Property(x => x.Status).IsRequired();

			builder.Property(x => x.CreatedDate_Utc).IsRequired();
			builder.Property(x => x.ModifiedDate_Utc).IsRequired();

			builder.HasOne(x => x.Avartar).WithMany(x => x.Users).HasForeignKey(x => x.AvatarId).OnDelete(DeleteBehavior.SetNull);
		}
	}
}
