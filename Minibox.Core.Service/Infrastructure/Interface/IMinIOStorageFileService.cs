using Minibox.Core.Data.Database.Main.Entity.Default;

namespace Minibox.Core.Service.Infrastructure.Interface
{
	public interface IMinIOStorageFileService
	{
		// Bucket operations
		/// <summary>
		/// Creates a bucket if it does not already exist.
		/// </summary>
		Task<bool> CreateBucketAsync(string bucketName);

		/// <summary>
		/// Checks if a bucket exists.
		/// </summary>
		Task<bool> BucketExistsAsync(string bucketName);

		/// <summary>
		/// Deletes a bucket (only if it is empty).
		/// </summary>
		Task<bool> DeleteBucketAsync(string bucketName);

		// File operations
		/// <summary>
		/// Uploads a file to MinIO.
		/// </summary>
		Task<string> UploadFileAsync(string bucketName, string objectName, Stream fileStream, string contentType);

		/// <summary>
		/// Uploads a file to MinIO and saves metadata in the database.
		/// </summary>
		/// <param name="uploadedBy">User ID who uploaded the file.</param>
		Task<string> UploadFileWithMetadataAsync(string bucketName, string objectName, Stream fileStream, string contentType, Guid uploadedBy);

		/// <summary>
		/// Downloads a file from MinIO.
		/// </summary>
		Task<(Stream, string)> DownloadFileAsync(string bucketName, string objectName);

		/// <summary>
		/// Deletes a file from MinIO.
		/// </summary>
		Task<bool> DeleteFileAsync(string bucketName, string objectName);

		/// <summary>
		/// Deletes a file from MinIO and removes its metadata from the database.
		/// </summary>
		Task<bool> DeleteFileByIdAsync(Guid fileId);

		/// <summary>
		/// Checks if a file exists in MinIO.
		/// </summary>
		Task<bool> FileExistsAsync(string bucketName, string objectName);

		/// <summary>
		/// Retrieves a list of files in a bucket.
		/// </summary>
		Task<IEnumerable<string>> ListFilesAsync(string bucketName);

		/// <summary>
		/// Gets the metadata of a file in MinIO.
		/// </summary>
		Task<IDictionary<string, string>> GetFileMetadataAsync(string bucketName, string objectName);

		/// <summary>
		/// Retrieves metadata of a stored file by its unique ID.
		/// </summary>
		Task<MinIOStorageFile?> GetFileMetadataByIdAsync(Guid fileId);

		// URL operations
		/// <summary>
		/// Gets the public URL of a file (if the bucket allows public access).
		/// </summary>
		string GetPublicFileUrl(string bucketName, string objectName);

		/// <summary>
		/// Generates a presigned URL for accessing a file with an expiration time.
		/// </summary>
		Task<string> GetPresignedUrlAsync(string bucketName, string objectName, int expiryInSeconds);

		// File movement operations
		/// <summary>
		/// Moves a file from one bucket to another.
		/// </summary>
		Task<bool> MoveFileAsync(string sourceBucket, string sourceObject, string destinationBucket, string destinationObject);

		/// <summary>
		/// Copies a file within MinIO.
		/// </summary>
		Task<bool> CopyFileAsync(string sourceBucket, string sourceObject, string destinationBucket, string destinationObject);
	}
}
