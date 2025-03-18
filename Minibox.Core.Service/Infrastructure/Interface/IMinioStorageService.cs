using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minibox.Core.Service.Infrastructure.Interface
{
	public interface IMinioStorageService
	{
		Task<bool> CreateBucketAsync(string bucketName);
		Task<string> UploadFileAsync(string bucketName, string objectName, Stream fileStream, string contentType);
		Task<(Stream, string)> DownloadFileAsync(string bucketName, string objectName);
	}
}
