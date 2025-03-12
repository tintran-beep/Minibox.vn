using Serilog;
using Microsoft.AspNetCore.Builder;
using Minibox.Shared.Module.Logging.Config;

namespace Minibox.Shared.Module.Logging.Extension
{
	public static class DependencyExtensions
	{
		public static void ConfigureSerilogLogging(this WebApplicationBuilder builder, string connectionString)
		{
			var env = builder.Environment.EnvironmentName;
			LoggerConfiguration loggerConfig;

			if (env == "Development")
			{
				loggerConfig = SerilogConsoleConfig.Configure();
			}
			else
			{
				loggerConfig = SerilogDatabaseConfig.Configure(connectionString);
			}

			Log.Logger = loggerConfig.CreateLogger();
			builder.Host.UseSerilog();
		}
	}
}
