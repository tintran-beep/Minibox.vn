using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibox.Shared.Model.ViewModel
{
	public class MediaVM
	{
		public Guid Id { get; set; }

		public int Type { get; set; }

		public string Url { get; set; } = string.Empty;
	}
}
