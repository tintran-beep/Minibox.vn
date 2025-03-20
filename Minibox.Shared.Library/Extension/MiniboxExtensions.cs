using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Minibox.Shared.Library.Const;

namespace Minibox.Shared.Library.Extension
{
	public class MiniboxExtensions
	{
		public static class SequentialGuidGenerator
		{
			public static Guid Generate()
			{
				var randomBytes = new byte[10];
				RandomNumberGenerator.Fill(randomBytes);

				var timestamp = BitConverter.GetBytes(DateTime.UtcNow.Ticks);

				var guidBytes = new byte[16];
				Buffer.BlockCopy(timestamp, 2, guidBytes, 0, 6);
				Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

				return new Guid(guidBytes);
			}
		}

		public static class RetryHelper
		{
			/// <summary>
			/// Retry
			/// </summary>
			/// <typeparam name="TResult"></typeparam>
			/// <param name="action"></param>
			/// <param name="retryInterval"></param>
			/// <param name="maxAttemptCount"></param>
			/// <returns></returns>
			/// <exception cref="AggregateException"></exception>
			public static async Task<TResult> RetryAsync<TResult>(Func<Task<TResult>> action, TimeSpan retryInterval, int maxAttemptCount = 3)
			{
				var exceptions = new List<Exception>();

				for (int attempted = 0; attempted < maxAttemptCount; attempted++)
				{
					try
					{
						if (attempted > 0)
						{
							Task.Delay(retryInterval).Wait();
						}
						return await action();
					}
					catch (Exception ex)
					{
						exceptions.Add(ex);
					}
				}
				throw new AggregateException(exceptions);
			}
		}
	
		public static class DateTimeHelper
		{
			public static DateTime? ConvertFromUtc(DateTime? utcDate, string timeZoneId = MiniboxConstants.DefaultTimeZoneId)
			{
				if (!utcDate.HasValue)
				{
					return null;
				}

				if (utcDate.Value.Kind != DateTimeKind.Utc)
				{
					throw new ArgumentException("Input date must be in UTC.", nameof(utcDate));
				}

				try
				{
					if (string.IsNullOrWhiteSpace(timeZoneId))
						timeZoneId = MiniboxConstants.DefaultTimeZoneId;

					TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
					return TimeZoneInfo.ConvertTimeFromUtc(utcDate.Value, timeZone);
				}
				catch (TimeZoneNotFoundException)
				{
					throw new ArgumentException($"Invalid TimeZone ID: {timeZoneId}", nameof(timeZoneId));
				}
				catch (InvalidTimeZoneException)
				{
					throw new ArgumentException($"Corrupt TimeZone data for ID: {timeZoneId}", nameof(timeZoneId));
				}
			}
		}
	}
}
