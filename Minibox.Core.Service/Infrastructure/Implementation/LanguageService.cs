using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Minibox.Core.Data.Database.Main;
using Minibox.Core.Data.Database.Main.Entity.Lang;
using Minibox.Core.Data.Infrastructure.Interface;
using Minibox.Core.Service.Infrastructure.Interface;
using Minibox.Shared.Library.Setting;
using Minibox.Shared.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibox.Core.Service.Infrastructure.Implementation
{
	public class LanguageService(
		IMapper mapper,
		IOptions<MiniboxSettings> appSettings,
		IUnitOfWork<MainDbContext> mainUnitOfWork) : BaseService(mapper, appSettings, mainUnitOfWork), ILanguageService
	{
		public async Task<List<LanguageVM>> GetAllSupportedLanguagesAsync()
		{
			var languages = await _mainUnitOfWork.Repository<Language>().Query().ToListAsync();
			return _mapper.Map<List<LanguageVM>>(languages);
		}

		public async Task<Dictionary<string, string>> GetLanguagesByCodeAsync(string languageCode = "vi")
		{
			var keys = await _mainUnitOfWork.Repository<LanguageKey>().Query().ToListAsync();
			var translations = await _mainUnitOfWork.Repository<LanguageTranslation>().Where(x => x.Code == languageCode).ToListAsync();

			var result = keys.ToDictionary(
				key => key.Key,
				key =>
				{
					var translation = translations.FirstOrDefault(t => t.LanguageKeyId == key.Id);
					return translation?.Value ?? key.DefaultValue;
				});

			return result;
		}
	}
}
