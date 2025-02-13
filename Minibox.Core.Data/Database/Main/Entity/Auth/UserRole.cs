using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Minibox.Shared.Library.Const;

namespace Minibox.Core.Data.Database.Main.Entity.Auth
{
	public class UserRole(Guid userId, Guid roleId) : BaseEntity
	{
		public Guid UserId { get; set; } = userId;

		public Guid RoleId { get; set; } = roleId;

		public virtual User? User { get; set; }
		public virtual Role? Role { get; set; }
	}

	public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
	{
		public void Configure(EntityTypeBuilder<UserRole> builder)
		{
			builder.ToTable(nameof(UserRole), MiniboxConstants.DbSchema.Authentication);

			builder.HasKey(x => new { x.UserId, x.RoleId });

			builder.HasOne(x => x.User).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId);
			builder.HasOne(x => x.Role).WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId);
		}
	}
}
