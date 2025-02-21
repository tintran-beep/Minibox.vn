using AutoMapper;
using Microsoft.Extensions.Options;
using Minibox.Core.Data.Database.Main;
using Minibox.Core.Data.Infrastructure.Interface;
using Minibox.Shared.Library.Setting;

namespace Minibox.Core.Service.Infrastructure
{
	public class BaseService(
		IMapper mapper,
		IOptions<MiniboxSettings> appSettings,
		IUnitOfWork<MainDbContext> mainUnitOfWork)
	{
		protected readonly IMapper _mapper = mapper;
		protected readonly MiniboxSettings _appSettings = appSettings.Value;
		protected readonly IUnitOfWork<MainDbContext> _mainUnitOfWork = mainUnitOfWork;
	}
}
