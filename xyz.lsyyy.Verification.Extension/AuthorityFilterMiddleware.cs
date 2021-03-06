using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using xyz.lsyyy.Verification.Extension.Service;
using xyz.lsyyy.Verification.Util;

namespace xyz.lsyyy.Verification.Extension
{
	public class AuthorityFilterMiddleware
	{
		private readonly ILogger<AuthorityFilterMiddleware> log;
		private readonly RequestDelegate _next;
		private readonly Func<HttpContext, IServiceProvider, string> getTokenFunc;

		public AuthorityFilterMiddleware(
			RequestDelegate next,
			ILoggerFactory loggerFactory,
			Func<HttpContext, IServiceProvider, string> getTokenFunc)
		{
			_next = next;
			this.getTokenFunc = getTokenFunc;
			log = loggerFactory.CreateLogger<AuthorityFilterMiddleware>();
		}
		public Task Invoke(
			HttpContext context,
			AuthorizationTagService authorizationTagService,
			VerificationService verificationService,
			UserService userService,
			IServiceProvider serviceProvider)
		{
			string result;
			string controllerName = (string)context.GetRouteValue("controller");
			string actionName = (string)context.GetRouteValue("action");
			//查询用户信息
			string token = getTokenFunc(context, serviceProvider);
			userService.SetToken(token);
			//Task.Run(async () =>
			//{
			//	await userService.GetUserInfoAsync();
			//});
			//没有AuthorizationTagAttribute，直接放行
			if (!authorizationTagService.ActionHasTag(controllerName, actionName))
			{
				log.LogTrace($"{controllerName} {actionName} has no tag");
				return _next(context);
			}
			//id不为空时，再判断是否需要认证
			if (!string.IsNullOrWhiteSpace(token))
			{
				if (verificationService.AllowAccess(controllerName, actionName))
				{
					return _next(context);
				}
				result = WebResultHelper.JsonMessageResult("没有访问权限");
			}
			else
			{
				result = WebResultHelper.JsonMessageResult("请先登录");
			}
			context.Response.StatusCode = 401;
			context.Response.ContentType = "application/json";
			context.Response.WriteAsync(result);
			return Task.CompletedTask;
		}
	}
}
