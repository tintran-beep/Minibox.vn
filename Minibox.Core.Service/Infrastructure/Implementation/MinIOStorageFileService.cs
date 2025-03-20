using AutoMapper;
using Microsoft.Extensions.Options;
using Minibox.Core.Data.Database.Main;
using Minibox.Core.Data.Database.Main.Entity.Default;
using Minibox.Core.Data.Infrastructure.Implementation;
using Minibox.Core.Data.Infrastructure.Interface;
using Minibox.Core.Service.Infrastructure.Interface;
using Minibox.Shared.Library.Setting;
using Minibox.Shared.Module.Logging.Infrastructure.Interface;
using Minio;
using Minio.ApiEndpoints;
using Minio.DataModel.Args;
using Minio.Exceptions;
using System.Security.AccessControl;

namespace Minibox.Core.Service.Infrastructure.Implementation
{
	public class MinIOStorageFileService(
		IMapper mapper,
		IMiniboxLogger logger,
		IOptions<MiniboxSettings> appSettings,
		IUnitOfWork<MainDbContext> mainUnitOfWork,
		IMinioClient minioClient)
		: BaseService(mapper, logger, appSettings, mainUnitOfWork), IMinIOStorageFileService
	{
		private readonly IMinioClient _minioClient = minioClient;

		/// <inheritdoc/>
		public async Task<bool> CreateBucketAsync(string bucketName)
		{
			try
			{
				bool exists = await BucketExistsAsync(bucketName);
				if (!exists)
				{
					await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error creating bucket: {bucketName}", ex);
				return false;
			}
		}

		/// <inheritdoc/>
		public async Task<bool> BucketExistsAsync(string bucketName)
		{
			try
			{
				return await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error checking bucket existence: {bucketName}", ex);
				return false;
			}
		}

		/// <inheritdoc/>
		public async Task<bool> DeleteBucketAsync(string bucketName)
		{
			try
			{
				if (await BucketExistsAsync(bucketName))
				{
					await _minioClient.RemoveBucketAsync(new RemoveBucketArgs().WithBucket(bucketName));
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error deleting bucket: {bucketName}", ex);
				return false;
			}
		}

		/// <inheritdoc/>
		public async Task<string> UploadFileAsync(string bucketName, string objectName, Stream fileStream, string contentType)
		{
			try
			{
				await _minioClient.PutObjectAsync(new PutObjectArgs()
					.WithBucket(bucketName)
					.WithObject(objectName)
					.WithStreamData(fileStream)
					.WithContentType(contentType)
					.WithObjectSize(fileStream.Length));

				return GetPublicFileUrl(bucketName, objectName);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error uploading file: {objectName}", ex);
				return string.Empty;
			}
		}

		/// <inheritdoc/>
		public async Task<string> UploadFileWithMetadataAsync(string bucketName, string objectName, Stream fileStream, string contentType, Guid uploadedBy)
		{
			try
			{
				var fileUrl = await UploadFileAsync(bucketName, objectName, fileStream, contentType);
				var fileMetadata = new MinIOStorageFile
				{
					FileName = objectName,
					FilePath = fileUrl,
					ContentType = contentType,
					FileSize = fileStream.Length,
					StorageBucket = bucketName,
					UploadedBy = uploadedBy
				};
				await _mainUnitOfWork.Repository<MinIOStorageFile>().InsertAsync(fileMetadata);
				await _mainUnitOfWork.SaveChangesAsync();

				return fileUrl;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error uploading file with metadata: {objectName}", ex);
				return string.Empty;
			}
		}

		/// <inheritdoc/>
		public async Task<(Stream, string)> DownloadFileAsync(string bucketName, string objectName)
		{
			try
			{
				var stream = new MemoryStream();
				await _minioClient.GetObjectAsync(new GetObjectArgs()
					.WithBucket(bucketName)
					.WithObject(objectName)
					.WithCallbackStream(s => s.CopyTo(stream)));
				return (stream, objectName);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error downloading file: {objectName}", ex);
				return (Stream.Null, string.Empty);
			}
		}

		/// <inheritdoc/>
		public async Task<bool> DeleteFileAsync(string bucketName, string objectName)
		{
			try
			{
				await _minioClient.RemoveObjectAsync(new RemoveObjectArgs()
					.WithBucket(bucketName)
					.WithObject(objectName));
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error deleting file: {objectName}", ex);
				return false;
			}
		}

		/// <inheritdoc/>
		public async Task<bool> DeleteFileByIdAsync(Guid fileId)
		{
			try
			{
				var fileMetadata = await _mainUnitOfWork.Repository<MinIOStorageFile>().GetByIdAsync(fileId);
				if (fileMetadata == null)
					return false;

				await DeleteFileAsync(fileMetadata.StorageBucket!, fileMetadata.FileName);

				_mainUnitOfWork.Repository<MinIOStorageFile>().Delete(fileMetadata);
				await _mainUnitOfWork.SaveChangesAsync();

				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error deleting file: {fileId}", ex);
				return false;
			}			
		}

		/// <inheritdoc/>
		public async Task<bool> FileExistsAsync(string bucketName, string objectName)
		{
			try
			{
				await _minioClient.StatObjectAsync(new StatObjectArgs().WithBucket(bucketName).WithObject(objectName));
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error checking file existence: {bucketName}/{objectName}", ex);
				return false;
			}
		}

		/// <inheritdoc/>
		public async Task<IEnumerable<string>> ListFilesAsync(string bucketName)
		{
			var files = new List<string>();
			try
			{
				await foreach (var item in _minioClient.ListObjectsEnumAsync(new ListObjectsArgs().WithBucket(bucketName)))
				{
					files.Add(item.Key);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error listing files in bucket: {bucketName}", ex);
			}
			return files;
		}

		/// <inheritdoc/>
		public async Task<IDictionary<string, string>> GetFileMetadataAsync(string bucketName, string objectName)
		{
			try
			{
				var stat = await _minioClient.StatObjectAsync(new StatObjectArgs().WithBucket(bucketName).WithObject(objectName));
				return stat.MetaData;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error getting file metadata: {bucketName}/{objectName}", ex);
				return new Dictionary<string, string>();
			}
		}

		/// <inheritdoc/>
		public async Task<MinIOStorageFile?> GetFileMetadataByIdAsync(Guid fileId)
		{
			try
			{
				return await _mainUnitOfWork.Repository<MinIOStorageFile>().GetByIdAsync(fileId);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error fetching file metadata by ID: {fileId}", ex);
				return null;
			}
		}

		/// <inheritdoc/>
		public string GetPublicFileUrl(string bucketName, string objectName)
		{
			try
			{
				var endpoint = _appSettings.MinIOStorageSettings.Endpoint;
				var port = _appSettings.MinIOStorageSettings.Port;
				var useSSL = _appSettings.MinIOStorageSettings.UseSSL;
				return $"{(useSSL ? "https" : "http")}://{endpoint}:{port}/{bucketName}/{objectName}";
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error generating public file URL: {bucketName}/{objectName}", ex);
				return string.Empty;
			}
		}

		/// <inheritdoc/>
		public async Task<string> GetPresignedUrlAsync(string bucketName, string objectName, int expiryInSeconds)
		{
			try
			{
				return await _minioClient.PresignedGetObjectAsync(new PresignedGetObjectArgs()
					.WithBucket(bucketName)
					.WithObject(objectName)
					.WithExpiry(expiryInSeconds));
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error generating presigned URL: {bucketName}/{objectName}", ex);
				return string.Empty;
			}
		}

		/// <inheritdoc/>
		public async Task<bool> MoveFileAsync(string sourceBucket, string sourceObject, string destinationBucket, string destinationObject)
		{
			try
			{
				await CopyFileAsync(sourceBucket, sourceObject, destinationBucket, destinationObject);
				return await DeleteFileAsync(sourceBucket, sourceObject);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error moving file from {sourceBucket}/{sourceObject} to {destinationBucket}/{destinationObject}", ex);
				return false;
			}
		}

		/// <inheritdoc/>
		public async Task<bool> CopyFileAsync(string sourceBucket, string sourceObject, string destinationBucket, string destinationObject)
		{
			try
			{
				var copySourceObjectArgs = new CopySourceObjectArgs()
					.WithBucket(sourceBucket)
					.WithObject(sourceObject);

				var copyObjectArgs = new CopyObjectArgs()
					.WithBucket(destinationBucket)
					.WithObject(destinationObject)
					.WithCopyObjectSource(copySourceObjectArgs);

				await _minioClient.CopyObjectAsync(copyObjectArgs);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error copying file: {sourceObject}", ex);
				return false;
			}
		}
	}
}
