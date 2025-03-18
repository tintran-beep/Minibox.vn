using AutoMapper;
using Microsoft.Extensions.Options;
using Minibox.Core.Data.Database.Main;
using Minibox.Core.Data.Infrastructure.Interface;
using Minibox.Core.Service.Infrastructure.Interface;
using Minibox.Shared.Library.Setting;
using Minibox.Shared.Module.Logging.Infrastructure.Interface;
using Minio;
using Minio.DataModel.Args;
using Serilog;

namespace Minibox.Core.Service.Infrastructure.Implementation
{
	public class MinioStorageService(
		IMapper mapper,
		IMiniboxLogger logger,
		IOptions<MiniboxSettings> appSettings,
		IUnitOfWork<MainDbContext> mainUnitOfWork,
		IMinioClient minioClient)
		: BaseService(mapper, logger, appSettings, mainUnitOfWork), IMinioStorageService
	{
		private readonly IMinioClient _minioClient = minioClient;
		public async Task<bool> CreateBucketAsync(string bucketName)
		{
			try
			{
				var makeBucketArgs = new MakeBucketArgs().WithBucket(bucketName);
				await _minioClient.MakeBucketAsync(makeBucketArgs);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error creating bucket: {bucketName}", ex);
				return false;
			}
		}

		public async Task<string> UploadFileAsync(string bucketName, string objectName, Stream fileStream, string contentType)
		{
			try
			{
				var putObjectArgs = new PutObjectArgs()
					.WithBucket(bucketName)
					.WithObject(objectName)
					.WithStreamData(fileStream)
					.WithObjectSize(fileStream.Length)
					.WithContentType(contentType);

				await _minioClient.PutObjectAsync(putObjectArgs);

				var minioEndpoint = _appSettings.MinIOStorageSettings.Endpoint;
				var minioPort = _appSettings.MinIOStorageSettings.Port;
				var useSSL = _appSettings.MinIOStorageSettings.UseSSL;

				string fullFileUrl = $"{(useSSL ? "https" : "http")}://{minioEndpoint}:{minioPort}/{bucketName}/{objectName}";

				return fullFileUrl;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error uploading file: {ex.Message}", ex);
				return string.Empty;
			}
		}


		public async Task<(Stream, string)> DownloadFileAsync(string bucketName, string objectName)
		{
			try
			{
				MemoryStream memoryStream = new();
				var getObjectArgs = new GetObjectArgs()
					.WithBucket(bucketName)
					.WithObject(objectName)
					.WithCallbackStream(stream => stream.CopyTo(memoryStream));

				await _minioClient.GetObjectAsync(getObjectArgs);
				memoryStream.Position = 0;

				return (memoryStream, "application/octet-stream");
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error downloading file: {ex.Message}", ex);
				return (new MemoryStream(), string.Empty);
			}
		}
	}
}
