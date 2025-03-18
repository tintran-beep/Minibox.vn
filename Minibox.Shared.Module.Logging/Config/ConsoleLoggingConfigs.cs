using Serilog.Sinks.SystemConsole.Themes;
using Serilog;

namespace Minibox.Shared.Module.Logging.Config
{
	public static class ConsoleLoggingConfigs
	{
		public static ILogger ConfigureConsoleLogger()
		{
			return new LoggerConfiguration().WriteTo.Console(theme: AnsiConsoleTheme.Literate)
													.CreateLogger();
		}
	}
}
