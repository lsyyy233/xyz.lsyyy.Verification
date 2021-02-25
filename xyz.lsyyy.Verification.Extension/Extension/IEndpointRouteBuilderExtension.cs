using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace xyz.lsyyy.Verification.Extension
{
	public static class IEndpointRouteBuilderExtension
	{
		public static void MapPushTagEndpoint(this IEndpointRouteBuilder endpoints,string route = "/PushTag")
		{
			endpoints.MapPost(route, async context =>
			{
				AuthorizationTagService authorizationTagService =
					context.RequestServices.GetService<AuthorizationTagService>();
				authorizationTagService.PushActionMap();
				await context.Response.WriteAsync("success");
			});
		}
	}
}
