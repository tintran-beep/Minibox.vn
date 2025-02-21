using Microsoft.Extensions.DependencyInjection;

namespace Minibox.Shared.Module.Mapping.Extension
{
	public static class MappingExtensions
	{
		public static IServiceCollection AddMappingModule(this IServiceCollection services)
		{
			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
			return services;
		}
	}
}
