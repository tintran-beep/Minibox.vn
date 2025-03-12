using Minibox.Shared.Model.ViewModel;

namespace Minibox.Core.Service.Infrastructure.Interface
{
	public interface ILanguageService
	{
		Task<List<LanguageVM>> GetAllSupportedLanguagesAsync();
		Task<Dictionary<string, string>> GetLanguagesByCodeAsync(string languageCode = "vi");
	}
}
