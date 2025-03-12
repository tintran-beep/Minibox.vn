using Serilog.Sinks.SystemConsole.Themes;
using Serilog;

namespace Minibox.Shared.Module.Logging.Config
{
	public static class SerilogConsoleConfig
	{
		public static LoggerConfiguration Configure()
		{
			return new LoggerConfiguration()
				.WriteTo.Console(theme: AnsiConsoleTheme.Literate,
					outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
				.Enrich.FromLogContext()
				.MinimumLevel.Debug();
		}
	}
}
