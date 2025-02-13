using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Minibox.Shared.Library.Const;

namespace Minibox.Core.Data.Database.Main.Entity.Auth
{
	public class UserClaim(Guid userId, Guid claimId) : BaseEntity
	{
		public Guid UserId { get; set; } = userId;

		public Guid ClaimId { get; set; } = claimId;

		public virtual User? User { get; set; }
		public virtual Claim? Claim { get; set; }
	}

	public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
	{
		public void Configure(EntityTypeBuilder<UserClaim> builder)
		{
			builder.ToTable(nameof(UserClaim), MiniboxConstants.DbSchema.Authentication);

			builder.HasKey(x => new { x.UserId, x.ClaimId });

			builder.HasOne(x => x.User).WithMany(x => x.UserClaims).HasForeignKey(x => x.UserId);
			builder.HasOne(x => x.Claim).WithMany(x => x.UserClaims).HasForeignKey(x => x.ClaimId);
		}
	}
}
