using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minibox.Core.Service.Infrastructure.Interface;

namespace Minibox.App.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LanguageController(ILanguageService languageService) : ControllerBase
	{
		private readonly ILanguageService _languageService = languageService;

		[HttpGet]
		public async Task<IActionResult> GetAllLanguages()
		{
			try
			{
				var languages = await _languageService.GetAllSupportedLanguagesAsync();
				return Ok(languages);
			}
			catch (Exception)
			{

				throw;
			}
		}

		[HttpGet("{languageCode}")]
		public async Task<IActionResult> GetLanguagesByCode(string languageCode = "vi")
		{
			try
			{
				var result = await _languageService.GetLanguagesByCodeAsync(languageCode);
				return Ok(result);
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
