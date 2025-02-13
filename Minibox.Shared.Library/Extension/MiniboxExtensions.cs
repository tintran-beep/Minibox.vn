using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

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
	}
}
