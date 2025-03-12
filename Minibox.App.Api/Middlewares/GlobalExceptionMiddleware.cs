using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace Minibox.App.Api.Middlewares
{
	public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
	{
		private readonly RequestDelegate _next = next;
		private readonly ILogger<GlobalExceptionMiddleware> _logger = logger;

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (TimeoutException ex)
			{
				_logger.LogWarning(ex, "⏳ The request timed out.");
				await HandleExceptionAsync(context, HttpStatusCode.GatewayTimeout, "The request timed out. Please try again later.");
			}
			catch (DbUpdateException ex)
			{
				_logger.LogError(ex, "❌ A database error occurred.");
				await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "A database error occurred while processing your request.");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "⚠️ An unexpected error occurred.");
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
