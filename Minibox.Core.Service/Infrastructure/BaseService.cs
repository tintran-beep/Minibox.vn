using AutoMapper;
using Microsoft.Extensions.Options;
using Minibox.Core.Data.Database.Main;
using Minibox.Core.Data.Infrastructure.Interface;
using Minibox.Shared.Library.Setting;
using Minibox.Shared.Module.Logging.Infrastructure.Interface;
using Serilog;

namespace Minibox.Core.Service.Infrastructure
{
	public class BaseService(
		IMapper mapper,
		IMiniboxLogger logger,
		IOptions<MiniboxSettings> appSettings,
		IUnitOfWork<MainDbContext> mainUnitOfWork)
	{
		protected readonly IMapper _mapper = mapper;
		protected readonly IMiniboxLogger _logger = logger;
		protected readonly MiniboxSettings _appSettings = appSettings.Value;
		protected readonly IUnitOfWork<MainDbContext> _mainUnitOfWork = mainUnitOfWork;
	}
}
