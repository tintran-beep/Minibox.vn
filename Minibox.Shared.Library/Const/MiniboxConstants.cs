using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Minibox.Shared.Library.Const
{
	public static class MiniboxConstants
	{
		public static class DbSchema
		{
			public const string Default = "dbo";
			public const string Authentication = "auth";
		}

		public static class Language
		{
			[Description("English")]
			public const string English = "en";

			[Description("Tiếng Việt")]
			public const string Vietnamese = "vi";

			public static string[] GetSupportedLanguages()
			{
				var languages = typeof(Language).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
												.Where(x => x.IsLiteral && !x.IsInitOnly && x.FieldType == typeof(string))
												.Select(x => x.GetRawConstantValue()?.ToString() ?? string.Empty)
												.ToArray();

				return languages ?? Array.Empty<string>();
			}
		}
	}
}
