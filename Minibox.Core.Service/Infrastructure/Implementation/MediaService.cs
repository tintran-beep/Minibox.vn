using AutoMapper;
using Microsoft.Extensions.Options;
using Minibox.Core.Data.Database.Main;
using Minibox.Core.Data.Infrastructure.Interface;
using Minibox.Core.Service.Infrastructure.Interface;
using Minibox.Shared.Library.Setting;
using Minibox.Shared.Model.BaseModel;
using Minibox.Shared.Model.ViewModel;
using Minibox.Shared.Module.Logging.Infrastructure.Interface;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibox.Core.Service.Infrastructure.Implementation
{
	public class MediaService(
		IMapper mapper,
		IMiniboxLogger logger,
		IOptions<MiniboxSettings> appSettings,
		IUnitOfWork<MainDbContext> mainUnitOfWork)
		: BaseService(mapper, logger, appSettings, mainUnitOfWork), IMediaService
	{
		public Task<ResponseModel<MediaVM>> GetAsync(string url)
		{
			throw new Exception();
		}
	}
}
