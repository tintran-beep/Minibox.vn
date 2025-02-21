using AutoMapper;
using Microsoft.Extensions.Options;
using Minibox.Core.Data.Database.Main;
using Minibox.Core.Data.Infrastructure.Interface;
using Minibox.Core.Service.Infrastructure.Interface;
using Minibox.Shared.Library.Setting;
using Minibox.Shared.Model.BaseModel;
using Minibox.Shared.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibox.Core.Service.Infrastructure.Implementation
{
	public class MediaService(
		IMapper mapper,
		IOptions<MiniboxSettings> appSettings,
		IUnitOfWork<MainDbContext> mainUnitOfWork) : BaseService(mapper, appSettings, mainUnitOfWork), IMediaService
	{
		public async Task<ResponseModel<MediaVM>> GetAsync(string url)
		{
			throw new Exception();
		}
	}
}
