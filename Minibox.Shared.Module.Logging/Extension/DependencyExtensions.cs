using Serilog;
using Microsoft.AspNetCore.Builder;
using Minibox.Shared.Module.Logging.Config;
using Microsoft.Extensions.DependencyInjection;
using Minibox.Shared.Module.Logging.Infrastructure.Implementation;
using Minibox.Shared.Module.Logging.Infrastructure.Interface;
using Microsoft.AspNetCore.Http;

namespace Minibox.Shared.Module.Logging.Extension
{
	public static class DependencyExtensions
	{
		public static void ConfigureSerilogLogging(this WebApplicationBuilder builder, string connectionString)
		{
			if (builder.Environment.EnvironmentName == "Development")
			{
				Log.Logger = ConsoleLoggingConfigs.ConfigureConsoleLogger();
			}
			else
			{
				var httpContextAccessor = builder.Services.BuildServiceProvider().GetRequiredService<IHttpContextAccessor>();

				Log.Logger = DatabaseLoggingConfigs.ConfigureDatabaseLogger(connectionString, httpContextAccessor);
			}

			builder.Host.UseSerilog();
			builder.Services.AddSingleton<IMiniboxLogger, MiniboxLogger>();
		}
	}
}
