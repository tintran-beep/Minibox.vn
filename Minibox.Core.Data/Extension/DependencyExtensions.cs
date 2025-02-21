using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Minibox.Core.Data.Database.Main;
using Minibox.Core.Data.Infrastructure.Interface;
using Minibox.Core.Data.Infrastructure.Implementation;

namespace Minibox.Core.Data.Extension
{
	public static class DependencyExtensions
	{
		public static IServiceCollection AddMainDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<MainDbContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString(nameof(MainDbContext)),
				x => x.MigrationsAssembly("Minibox.Core.Data")));

			return services;
		}

		public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
		{
			return services.AddScoped<IUnitOfWork<MainDbContext>, UnitOfWork<MainDbContext>>();
		}

		public static async Task<IServiceProvider> MigrateAsync(this IServiceProvider service)
		{
			using (var scope = service.CreateScope())
			{
				using var mainDbContext = scope.ServiceProvider.GetRequiredService<MainDbContext>();
				await mainDbContext.Database.MigrateAsync();
			}
			return service;
		}
	}
}
