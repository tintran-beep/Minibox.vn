using Minibox.Core.Data.Database.Main.Entity.Default;
using Minibox.Shared.Model.ViewModel;

namespace Minibox.Shared.Module.Mapping.Profile
{
	public class MediaMappingProfile : AutoMapper.Profile
	{
		public MediaMappingProfile() 
		{
			CreateMap<Media, MediaVM>().ReverseMap();
		}
	}
}
