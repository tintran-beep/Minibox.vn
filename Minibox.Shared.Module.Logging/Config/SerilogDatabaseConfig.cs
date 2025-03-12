using Serilog.Sinks.MSSqlServer;
using Serilog;

namespace Minibox.Shared.Module.Logging.Config
{
	public static class SerilogDatabaseConfig
	{
		public static LoggerConfiguration Configure(string connectionString)
		{
			var columnOptions = new ColumnOptions();
			columnOptions.Store.Remove(StandardColumn.Properties);
			columnOptions.Store.Add(StandardColumn.LogEvent);

			return new LoggerConfiguration().WriteTo.MSSqlServer(
					  connectionString,
					  sinkOptions: new MSSqlServerSinkOptions
					  {
						  TableName = "Log",
						  AutoCreateSqlTable = true
					  },
					  columnOptions: columnOptions)
				  .Enrich.FromLogContext()
				  .MinimumLevel.Information();
		}
	}
}
