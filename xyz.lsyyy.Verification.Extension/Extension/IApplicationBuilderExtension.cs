using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;

namespace xyz.lsyyy.Verification.Extension
{

	public static class IApplicationBuilderExtension
	{
		/// <summary>
		/// 添加授权中间件
		/// </summary>
		/// <param name="app"></param>
		/// <param name="getUserIdFunc">通过HttpConext获取用户Id</param>
		public static void UseVerificationMiddleware(
			this IApplicationBuilder app,
			Func<HttpContext, IServiceProvider, string> getUserIdFunc = null)
		{
			app.UseMiddleware<AuthorityFilterMiddleware>(getUserIdFunc);
		}
	}
}
