using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibox.Core.Data.Database.Main
{
	public class MainDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
	{
		public MainDbContext CreateDbContext(string[] args)
		{
			var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

			var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory());

			if (env == "Development")
				configuration.AddJsonFile($"appsettings.{env}_{Environment.MachineName}.json", true, true);
			else
				configuration.AddJsonFile($"appsettings.{env}.json", true, true);

			var configurationBuilder = configuration.Build();

			var connectionString = configurationBuilder.GetConnectionString(nameof(MainDbContext)) ?? string.Empty;


			var optionsBuilder = new DbContextOptionsBuilder<MainDbContext>();

			optionsBuilder.UseSqlServer(connectionString);

			return new MainDbContext(optionsBuilder.Options);
		}
	}
}
