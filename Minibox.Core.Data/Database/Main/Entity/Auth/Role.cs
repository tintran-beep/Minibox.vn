using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Minibox.Shared.Library.Extension;
using Minibox.Shared.Library.Const;

namespace Minibox.Core.Data.Database.Main.Entity.Auth
{
	public class Role : BaseEntity
	{
		public Role() 
		{
			Id = MiniboxExtensions.SequentialGuidGenerator.Generate();
		}

		public Guid Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public virtual ICollection<RoleClaim> RoleClaims { get; set; } = [];
		public virtual ICollection<UserRole> UserRoles { get; set; } = [];
	}

	public class RoleConfiguration : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			builder.ToTable(nameof(Role), MiniboxConstants.DbSchema.Authentication);

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
			builder.Property(x => x.Description).HasMaxLength(500).IsRequired();
		}
	}
}
