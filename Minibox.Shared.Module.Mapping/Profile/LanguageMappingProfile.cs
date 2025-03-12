using Minibox.Core.Data.Database.Main.Entity.Lang;
using Minibox.Shared.Model.ViewModel;

namespace Minibox.Shared.Module.Mapping.Profile
{
	public class LanguageMappingProfile : AutoMapper.Profile
	{
		public LanguageMappingProfile()
		{
			CreateMap<Language, LanguageVM>().ReverseMap();
			CreateMap<LanguageKey, LanguageKeyVM>().ReverseMap();
			CreateMap<LanguageTranslation, LanguageTranslationVM>().ReverseMap();
		}
	}
}
