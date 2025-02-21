using Minibox.Shared.Model.ViewModel;
using Minibox.Shared.Model.BaseModel;

namespace Minibox.Core.Service.Infrastructure.Interface
{
	public interface IMediaService
	{
		Task<ResponseModel<MediaVM>> GetAsync(string url);
	}
}
