using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Minibox.Shared.Library.Const;
using Minibox.Shared.Library.Extension;

namespace Minibox.Core.Data.Database.Main.Entity.Auth
{
	public class Claim : BaseEntity
	{
		public Claim() 
		{
			Id = MiniboxExtensions.SequentialGuidGenerator.Generate();
		}

		public Guid Id { get; set; }

		public string Type { get; set; } = string.Empty;

		public string Value { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public virtual ICollection<RoleClaim> RoleClaims { get; set; } = [];
		public virtual ICollection<UserClaim> UserClaims { get; set; } = [];
	}

	public class ClaimConfiguration : IEntityTypeConfiguration<Claim>
	{
		public void Configure(EntityTypeBuilder<Claim> builder)
		{
			builder.ToTable(nameof(Claim), MiniboxConstants.DbSchema.Authentication);

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Type).HasMaxLength(100).IsRequired();
			builder.Property(x => x.Value).HasMaxLength(100).IsRequired();
			builder.Property(x => x.Description).HasMaxLength(500).IsRequired();
		}
	}
}
