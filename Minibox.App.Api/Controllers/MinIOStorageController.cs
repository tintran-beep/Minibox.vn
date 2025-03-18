﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minibox.Core.Service.Infrastructure.Interface;

namespace Minibox.App.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MinIOStorageController(
		IMinioStorageService minioStorageService
		) : ControllerBase
	{
		private readonly IMinioStorageService _minioStorageService = minioStorageService;

		[HttpPost("create-bucket/{bucketName}")]
		public async Task<IActionResult> CreateBucket(string bucketName)
		{
			var result = await _minioStorageService.CreateBucketAsync(bucketName);
			if (result)
				return Ok(new { message = $"Bucket '{bucketName}' created successfully!" });

			return BadRequest(new { message = "Failed to create bucket." });
		}

		[HttpPost("upload/{bucketName}")]
		public async Task<IActionResult> UploadFile(string bucketName, IFormFile file)
		{
			if (file == null || file.Length == 0)
				return BadRequest(new { message = "Invalid file." });

			string objectName = $"{Guid.NewGuid()}_{file.FileName}";
			using var stream = file.OpenReadStream();

			var fileUrl = await _minioStorageService.UploadFileAsync(bucketName, objectName, stream, file.ContentType);
			if (fileUrl != null)
				return Ok(new { message = "File uploaded successfully!", url = fileUrl });

			return BadRequest(new { message = "File upload failed." });
		}

		[HttpGet("download/{bucketName}/{objectName}")]
		public async Task<IActionResult> DownloadFile(string bucketName, string objectName)
		{
			var (fileStream, contentType) = await _minioStorageService.DownloadFileAsync(bucketName, objectName);
			if (fileStream == null)
				return NotFound(new { message = "File not found or download failed." });

			return File(fileStream, contentType, objectName);
		}
	}
}
