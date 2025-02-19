using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibox.Shared.Library.Enum
{
	public static class MiniboxEnums
	{
		public enum UserStatus : int
		{
			New = 0,
			Active = 1,
			InActive = 2
		}

		public enum MediaType : int
		{
			Image = 0,
			Video = 1,
		}
	}
}
