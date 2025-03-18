using Minibox.Shared.Module.Logging.Infrastructure.Interface;
using Serilog;

namespace Minibox.Shared.Module.Logging.Infrastructure.Implementation
{
	public class MiniboxLogger : IMiniboxLogger
	{
		public void LogInformation(string message)
		{
			Log.Information(message);
		}

		public void LogWarning(string message)
		{
			Log.Warning(message);
		}

		public void LogError(string message, Exception? ex = null)
		{
			Log.Error(ex, message);
		}

		public void LogDebug(string message)
		{
			Log.Debug(message);
		}
	}
}
