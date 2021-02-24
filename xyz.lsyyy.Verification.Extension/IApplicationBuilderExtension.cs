using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace xyz.lsyyy.Verification.Extension
{
	public static class IApplicationBuilderExtension
	{
		public static void UseVerificationMiddleware(this IApplicationBuilder app, 
			Func<HttpContext, IServiceProvider, string> getUserIdFunc = null,
			string pushTagRoute = "/PushTag")
		{
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapPost(pushTagRoute, async context =>
				{
					AuthorizationTagService authorizationTagService =
						context.RequestServices.GetService<AuthorizationTagService>();
					authorizationTagService.PushActionMap();
					await context.Response.WriteAsync("success");
				});
			});
			app.UseMiddleware<AuthorityFilterMiddleware>(getUserIdFunc);
		}
	}
}
