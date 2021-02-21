using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using xyz.lsyyy.Verification.Extension;

namespace xyz.lsyyy.Verification.Test
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();
			services.AddVerificationService((s,o) =>
			{
				o.Address = new Uri(Configuration.GetValue<string>("VerificationServer"));
			});
			services.AddLogging();
			services.AddDistributedMemoryCache();
			services.AddSession(options =>
			{
				options.IdleTimeout = TimeSpan.FromMinutes(5);
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
				options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
			});
			services.AddRouting(x =>
			{
				x.LowercaseQueryStrings = true;
				x.LowercaseUrls = true;
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler(handler =>
				{
					handler.Run(async context =>
					{
						context.Response.StatusCode = 500;
						await context.Response.WriteAsync("Unexpected Error");
					});
				});
			}
			app.UseStaticFiles();
			app.UseRouting();
			app.UseSession();
			app.UseVerificationMiddleware((context,serviceProvider) =>
				context.Session.GetString("UserId")
			);
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
