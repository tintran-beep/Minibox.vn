using Minibox.Core.Data.Database.Main.Entity.Auth;
using Minibox.Shared.Model.ViewModel;

namespace Minibox.Shared.Module.Mapping.Profile
{
	public class AuthenticationMappingProfile : AutoMapper.Profile
	{
		public AuthenticationMappingProfile()
		{
			CreateMap<Role, RoleVM>().ReverseMap();
			CreateMap<Claim, ClaimVM>().ReverseMap();
		}
	}
}
