using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Minibox.App.Api.Attributes
{
	[AttributeUsage(AttributeTargets.Method)]
	public class RequireRolesOrClaimsAttribute(string[] roles, string[] claims) : Attribute, IAsyncAuthorizationFilter
	{
		private readonly string[] _roles = roles;
		private readonly string[] _claims = claims;

		public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
		{
			var user = context.HttpContext.User;

			if (user == null || !user.Identity?.IsAuthenticated == true)
			{
				context.Result = new UnauthorizedResult();
				return;
			}

			bool hasRole = _roles.Any(user.IsInRole);
			bool hasClaim = _claims.Any(c =>
			{
				var parts = c.Split('/');
				return parts.Length == 2 && user.HasClaim(parts[0], parts[1]);
			});

			if (!hasRole && !hasClaim)
			{
				context.Result = new ForbidResult();
			}

			await Task.CompletedTask;
		}
	}
}
