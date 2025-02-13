using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Minibox.Shared.Library.Const;

namespace Minibox.Core.Data.Database.Main.Entity.Auth
{
	public class RoleClaim(Guid roleId, Guid claimId) : BaseEntity
	{
		public Guid RoleId { get; set; } = roleId;

		public Guid ClaimId { get; set; } = claimId;

		public virtual Role? Role { get; set; }

		public virtual Claim? Claim { get; set; }
	}

	public class RoleClaimConfiguration : IEntityTypeConfiguration<RoleClaim>
	{
		public void Configure(EntityTypeBuilder<RoleClaim> builder)
		{
			builder.ToTable(nameof(RoleClaim), MiniboxConstants.DbSchema.Authentication);

			builder.HasKey(x => new { x.RoleId, x.ClaimId });

			builder.HasOne(x => x.Role).WithMany(x => x.RoleClaims).HasForeignKey(x => x.RoleId);
			builder.HasOne(x => x.Claim).WithMany(x => x.RoleClaims).HasForeignKey(x => x.ClaimId);
		}
	}
}
