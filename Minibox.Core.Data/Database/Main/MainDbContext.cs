using Microsoft.EntityFrameworkCore;
using Minibox.Core.Data.Database.Main.Entity.Auth;
using Minibox.Core.Data.Database.Main.Entity.Default;

namespace Minibox.Core.Data.Database.Main
{
	public class MainDbContext(DbContextOptions options) : BaseDbContext(options)
	{
		#region Auth
		public virtual DbSet<Claim> Claims { get; set; }
		public virtual DbSet<Role> Role { get; set; }
		public virtual DbSet<RoleClaim> RoleClaim { get; set; }
		public virtual DbSet<User> User { get; set; }
		public virtual DbSet<UserClaim> UserClaim { get; set; }
		public virtual DbSet<UserLogin> UserLogin { get; set; }
		public virtual DbSet<UserRole> UserRole { get; set; }
		public virtual DbSet<UserToken> UserToken { get; set; }
		#endregion

		#region Default: dbo
		public virtual DbSet<Media> Media { get; set; }
		#endregion

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			#region Auth

			new ClaimConfiguration().Configure(builder.Entity<Claim>());

			new RoleConfiguration().Configure(builder.Entity<Role>());

			new RoleClaimConfiguration().Configure(builder.Entity<RoleClaim>());

			new UserConfiguration().Configure(builder.Entity<User>());

			new UserClaimConfiguration().Configure(builder.Entity<UserClaim>());

			new UserLoginConfiguration().Configure(builder.Entity<UserLogin>());

			new UserRoleConfiguration().Configure(builder.Entity<UserRole>());

			new UserTokenConfiguration().Configure(builder.Entity<UserToken>());
			#endregion

			#region Default: dbo

			new MediaConfiguration().Configure(builder.Entity<Media>());
			#endregion
		}
	}
}
