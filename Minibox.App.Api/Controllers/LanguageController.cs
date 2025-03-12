using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minibox.Core.Service.Infrastructure.Interface;

namespace Minibox.App.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LanguageController(
		ILogger<LanguageController> logger,
		ILanguageService languageService) : ControllerBase
	{
		private readonly ILogger<LanguageController> _logger = logger;
		private readonly ILanguageService _languageService = languageService;

		[HttpGet]
		public async Task<IActionResult> GetAllLanguages()
		{
			var languages = await _languageService.GetAllSupportedLanguagesAsync();
			return Ok(languages);
		}

		[HttpGet("{languageCode}")]
		public async Task<IActionResult> GetLanguagesByCode(string languageCode = "vi")
		{
			var result = await _languageService.GetLanguagesByCodeAsync(languageCode);
			return Ok(result);
		}
	}
}
