using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibox.Shared.Model.ViewModel
{
	public class ClaimVM
	{
		public Guid Id { get; set; }

		public string Type { get; set; } = string.Empty;

		public string Value { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;
	}
}
