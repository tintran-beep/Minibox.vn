using Serilog.Sinks.MSSqlServer;
using Serilog;
using System.Collections.ObjectModel;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace Minibox.Shared.Module.Logging.Config
{
	public static class DatabaseLoggingConfigs
	{
		public static ILogger ConfigureDatabaseLogger(string connectionString, IHttpContextAccessor httpContextAccessor)
		{
			var columnOptions = new ColumnOptions
			{
				AdditionalColumns = new Collection<SqlColumn>
				{
					new("UserId", SqlDbType.UniqueIdentifier),
					new("RequestId", SqlDbType.NVarChar, true, 100),
					new("SourceContext", SqlDbType.NVarChar, true, 255),
					new("MachineName", SqlDbType.NVarChar, true, 255),
					new("IPAddress", SqlDbType.NVarChar, true, 50),
					new("Properties", SqlDbType.NVarChar, true, 1000),
					new("RequestPath", SqlDbType.NVarChar, true, 500),
					new("StatusCode", SqlDbType.Int)
				}
			};

			var httpContext = httpContextAccessor.HttpContext;
			string clientIp = httpContext != null ? GetClientIPAddress(httpContext) : "Unknown";

			return new LoggerConfiguration()
								.Enrich.WithMachineName()
								.Enrich.WithProperty("IPAddress", clientIp)
								.Enrich.FromLogContext()
								.WriteTo.MSSqlServer(
									connectionString,
									sinkOptions: new MSSqlServerSinkOptions
									{
										TableName = "Log",
										AutoCreateSqlTable = false
									},
									columnOptions: columnOptions
								)
								.CreateLogger();
		}

		private static string GetClientIPAddress(HttpContext httpContext)
		{
			if (httpContext == null)
			{
				return "Unknown";
			}

			var forwardedHeader = httpContext.Request.Headers["X-Forwarded-For"];
			var ip = forwardedHeader.Count > 0 ? forwardedHeader[0] : string.Empty;

			if (string.IsNullOrEmpty(ip))
			{
				ip = httpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
			}

			if (ip == "::1")
			{
				ip = "127.0.0.1";
			}

			return string.IsNullOrEmpty(ip) ? "Unknown" : ip;
		}
	}
}
