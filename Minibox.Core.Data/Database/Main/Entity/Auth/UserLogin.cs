using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Minibox.Shared.Library.Const;

namespace Minibox.Core.Data.Database.Main.Entity.Auth
{
	public class UserLogin : BaseEntity
	{
		public UserLogin() 
		{
		
		}

		public Guid UserId { get; set; }

		public string Provider { get; set; } = string.Empty;

		public string ProviderKey { get; set; } = string.Empty;

		public virtual User? User { get; set; }
	}

	public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
	{
		public void Configure(EntityTypeBuilder<UserLogin> builder)
		{
			builder.ToTable(nameof(UserLogin), MiniboxConstants.DbSchema.Authentication);

			builder.HasKey(x => new { x.Provider, x.ProviderKey });
			builder.Property(x => x.Provider).HasMaxLength(100).IsRequired();
			builder.Property(x => x.ProviderKey).HasMaxLength(100).IsRequired();

			builder.HasOne(x => x.User).WithMany(x => x.UserLogins).HasForeignKey(x => x.UserId);
		}
	}
}
