﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Minibox.Shared.Library.Setting;
using Minio;

namespace Minibox.Core.Service.Extension
{
	public static class DependencyExtensions
	{
		public static IServiceCollection AddBussinessLogicLayer(this IServiceCollection services)
		{
			var implementedServices = System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(x => !string.IsNullOrWhiteSpace(x.Name)
																										   && x.IsInterface == false
																										   && x.Name.EndsWith("Service")
																										   && x.GetInterfaces().Length > 0).ToList();
			if (implementedServices != null && implementedServices.Count != 0)
			{
				implementedServices.ForEach(assignedTypes =>
				{
					var serviceType = assignedTypes.GetInterfaces().FirstOrDefault(x => x.Name == $"I{assignedTypes.Name}");
					if (serviceType != null)
						services.AddScoped(serviceType, assignedTypes);
				});
			}

			return services;
		}

		public static IServiceCollection AddMinIOStorage(this IServiceCollection services)
		{			
			services.AddSingleton<IMinioClient>(sp =>
			{
				var settings = sp.GetRequiredService<IOptions<MiniboxSettings>>().Value.MinIOStorageSettings;

				return new MinioClient()
					.WithEndpoint(settings.Endpoint, settings.Port)
					.WithCredentials(settings.AccessKey, settings.SecretKey)
					.WithSSL(settings.UseSSL)
					.Build();
			});

			return services;
		}
	}
}
