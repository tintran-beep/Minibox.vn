using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Minibox.Shared.Library.Const;

namespace Minibox.Core.Data.Database.Main.Entity.Auth
{
	public class UserToken : BaseEntity
	{
		public UserToken() 
		{
		
		}

		public Guid UserId { get; set; }
		
		public string Provider { get; set; } = string.Empty;

		public string TokenName { get; set; } = string.Empty;

		public string TokenValue { get; set; } = string.Empty;

		public virtual User? User { get; set; }
	}

	public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
	{
		public void Configure(EntityTypeBuilder<UserToken> builder)
		{
			builder.ToTable(nameof(UserToken), MiniboxConstants.DbSchema.Authentication);

			builder.HasKey(x => new { x.UserId, x.Provider, x.TokenName });
			builder.Property(x=>x.Provider).HasMaxLength(100).IsRequired();
			builder.Property(x => x.TokenName).HasMaxLength(100).IsRequired();
			builder.Property(x => x.TokenValue).HasMaxLength(int.MaxValue).IsRequired();

			builder.HasOne(x => x.User).WithMany(x => x.UserTokens).HasForeignKey(x => x.UserId);
		}
	}
}
