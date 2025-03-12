using Serilog.Sinks.MSSqlServer;
using Serilog;

namespace Minibox.Shared.Module.Logging.Config
{
	public static class SerilogDatabaseConfig
	{
		public static LoggerConfiguration Configure(string connectionString)
		{
			return new LoggerConfiguration()
				.WriteTo.MSSqlServer(
					connectionString: connectionString,
					sinkOptions: new MSSqlServerSinkOptions
					{
						TableName = "Logs",
						AutoCreateSqlTable = true
					}
				)
				.Enrich.FromLogContext()
				.MinimumLevel.Information();
		}
	}
}
