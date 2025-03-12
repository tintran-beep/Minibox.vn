using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace Minibox.App.Api.Middlewares
{
	public class GlobalExceptionMiddleware(RequestDelegate next)
	{
		private readonly RequestDelegate _next = next;

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (TimeoutException)
			{
				await HandleExceptionAsync(context, HttpStatusCode.GatewayTimeout, "The request timed out. Please try again later.");
			}
			catch (DbUpdateException)
			{
				await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "A database error occurred while processing your request.");
			}
			catch (Exception)
			{
				await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, "An unexpected error occurred. Please try again later.");
			}
		}

		private static Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)statusCode;
			var result = JsonSerializer.Serialize(new { error = message });
			return context.Response.WriteAsync(result);
		}
	}
}
