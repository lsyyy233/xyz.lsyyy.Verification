using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using xyz.lsyyy.Verification.Extension.Service;

namespace xyz.lsyyy.Verification.Extension
{
	public class AuthorityFilterMiddleware
	{
		private readonly ILogger<AuthorityFilterMiddleware> log;
		private readonly RequestDelegate _next;
		private readonly Func<HttpContext, IServiceProvider, string> getUserIdFunc;

		public AuthorityFilterMiddleware(
			RequestDelegate next, 
			ILoggerFactory loggerFactory,
			AuthorizationTagService authorizationTagService,
			Func<HttpContext, IServiceProvider, string> getUserIdFunc)
		{
			_next = next;
			this.getUserIdFunc = getUserIdFunc;
			log = loggerFactory.CreateLogger<AuthorityFilterMiddleware>();
			authorizationTagService.PushActionMap();
		}
		public Task Invoke(
			HttpContext context,
			AuthorizationTagService authorizationTagService,
			VerificationService verificationService,
			IServiceProvider serviceProvider)
		{
			string controllerName = (string)context.GetRouteValue("controller");
			string actionName = (string)context.GetRouteValue("action");
			//没有AuthorizationTagAttribute，直接放行
			if (!authorizationTagService.ActionHasTag(controllerName, actionName))
			{
				log.LogTrace($"{controllerName} {actionName} has no tag");
				return _next(context);
			}
			string userId = getUserIdFunc(context, serviceProvider);
			//id不为空时，再判断是否需要认证
			log.LogInformation($"userId={userId}");
			if (!(string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(userId)))
			{
				if (verificationService.AllowAccess(controllerName, actionName, userId))
				{
					return _next(context);
				}
			}
			string result = JsonConvert.SerializeObject(new
			{
				Message = "没有访问权限"
			});
			context.Response.StatusCode = 401;
			context.Response.ContentType = "application/json";
			context.Response.WriteAsync(result);
			return Task.CompletedTask;
		}
	}
}
