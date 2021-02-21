using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using xyz.lsyyy.Verification;
using xyz.lsyyy.Verification.Data;
using xyz.lsyyy.Verification.Services;

namespace xyz.lsyyyVerification
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
			services.AddControllers()
				.AddNewtonsoftJson(option =>
				{
					option.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
				});
			services.AddDbContext<MyDbContext>(options =>
			{
				options
					.UseLazyLoadingProxies()
					.UseMySql(Configuration.GetConnectionString("DefaultDataBase"));
			});
			services.AddLogging();
			services.AddGrpc();
		}
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGrpcService<UserService>();
				endpoints.MapGrpcService<VerificationService>();
			});
		}
	}
}
