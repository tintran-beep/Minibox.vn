using Microsoft.EntityFrameworkCore;
using Minibox.Shared.Module.Logging.Infrastructure.Interface;
using System.Net;

namespace Minibox.App.Api.Middlewares
{
	public class GlobalExceptionMiddleware(RequestDelegate next, IMiniboxLogger logger)
	{
		private readonly RequestDelegate _next = next;
		private readonly IMiniboxLogger _logger = logger;

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (TimeoutException ex)
			{
				_logger.LogError("⏳ The request timed out.", ex);
				await HandleExceptionAsync(context, HttpStatusCode.GatewayTimeout, "The request timed out. Please try again later.");
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError("❌ A database error occurred.", ex);
				await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "A database error occurred while processing your request.");
			}
			catch (Exception ex)
			{
				_logger.LogError("⚠️ An unexpected error occurred.", ex);
				await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "An unexpected error occurred. Please try again later.");
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)statusCode;

			var response = new { error = message };
			return context.Response.WriteAsJsonAsync(response);
		}
	}
}
