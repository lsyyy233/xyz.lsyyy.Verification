using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;

namespace xyz.lsyyy.Verification.Extension
{
	public static class IApplicationBuilderExtension
	{
		public static void UseVerificationMiddleware(this IApplicationBuilder app, Func<HttpContext, IServiceProvider, string> getUserIdFunc = null)
		{
			if (getUserIdFunc == null)
			{
				throw new ArgumentNullException("Argument optionAction can not be null");
			}
			app.UseMiddleware<AuthorityFilterMiddleware>(getUserIdFunc);
		}
	}
}
