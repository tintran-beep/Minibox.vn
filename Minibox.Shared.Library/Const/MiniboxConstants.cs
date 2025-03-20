using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Minibox.Shared.Library.Const
{
	public static class MiniboxConstants
	{
		public const string AppName = "Minibox";
		public const string DefaultTimeZoneId = "SE Asia Standard Time"; //(UTC+07:00) Southeast Asia

		public static class DbSchema
		{
			public const string Default = "dbo";
			public const string Language = "lang";
			public const string Authentication = "auth";
		}

		public static class MinIOStorageFileContentType
		{
			// Documents
			public const string Pdf = "application/pdf";
			public const string Doc = "application/msword";
			public const string Docx = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
			public const string Xls = "application/vnd.ms-excel";
			public const string Xlsx = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
			public const string Ppt = "application/vnd.ms-powerpoint";
			public const string Pptx = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
			public const string Txt = "text/plain";
			public const string Csv = "text/csv";
			public const string Json = "application/json";
			public const string Xml = "application/xml";

			// Images
			public const string Jpeg = "image/jpeg";
			public const string Png = "image/png";
			public const string Gif = "image/gif";
			public const string Bmp = "image/bmp";
			public const string Webp = "image/webp";
			public const string Svg = "image/svg+xml";

			// Videos
			public const string Mp4 = "video/mp4";
			public const string Avi = "video/x-msvideo";
			public const string Mov = "video/quicktime";
			public const string Wmv = "video/x-ms-wmv";
			public const string Flv = "video/x-flv";
			public const string Mkv = "video/x-matroska";
			public const string Webm = "video/webm";

			// Audio
			public const string Mp3 = "audio/mpeg";
			public const string Wav = "audio/wav";
			public const string Ogg = "audio/ogg";
			public const string M4a = "audio/mp4";
			public const string Aac = "audio/aac";

			// Compressed Files
			public const string Zip = "application/zip";
			public const string Rar = "application/x-rar-compressed";
			public const string Tar = "application/x-tar";
			public const string Gz = "application/gzip";
			public const string SevenZip = "application/x-7z-compressed";

			// Web Files
			public const string Html = "text/html";
			public const string Css = "text/css";
			public const string Js = "application/javascript";

			// Fonts
			public const string Woff = "font/woff";
			public const string Woff2 = "font/woff2";
			public const string Ttf = "font/ttf";
			public const string Otf = "font/otf";

			// Other
			public const string Exe = "application/octet-stream";
			public const string Apk = "application/vnd.android.package-archive";
			public const string Iso = "application/x-iso9660-image";
		}

		public static class StatusCode
		{
			// 2xx Success
			public const int OK = 200;
			public const int Created = 201;
			public const int Accepted = 202;
			public const int NoContent = 204;

			// 3xx Redirection
			public const int MovedPermanently = 301;
			public const int Found = 302;
			public const int NotModified = 304;

			// 4xx Client Errors
			public const int BadRequest = 400;
			public const int Unauthorized = 401;
			public const int Forbidden = 403;
			public const int NotFound = 404;
			public const int MethodNotAllowed = 405;
			public const int Conflict = 409;
			public const int UnprocessableEntity = 422;

			// 5xx Server Errors
			public const int InternalServerError = 500;
			public const int BadGateway = 502;
			public const int ServiceUnavailable = 503;
			public const int GatewayTimeout = 504;
		}

		

        public static class Role
        {
			public const string Admin = "Admin";
			public const string Customer = "Customer";
			public const string Contributor = "Contributor";
		}

		public static class Claim
		{
			public const string Admin = "Admin";
			public const string Customer = "Customer";
			public const string Contributor = "Contributor";
		}
	}
}
