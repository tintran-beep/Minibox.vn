using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibox.Shared.Model.ViewModel
{
	public class LanguageVM
	{
		public Guid Id { get; set; }
		public string Code { get; set; } = string.Empty;
		public string Value { get; set; } = string.Empty;
	}

	public class LanguageKeyVM
	{
		public Guid Id { get; set; }
		public string Key { get; set; } = string.Empty;
		public string DefaultValue { get; set; } = string.Empty;
	}

	public class LanguageTranslationVM
	{
		public Guid Id { get; set; }
		public Guid LanguageKeyId { get; set; }
		public string Code { get; set; } = string.Empty;
		public string Value { get; set; } = string.Empty;
	}
}
