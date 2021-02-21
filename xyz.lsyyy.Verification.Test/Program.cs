using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;

namespace xyz.lsyyy.Verification.Test
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureLogging((context, logging) =>
				{
					LogManager.Configuration = new NLogLoggingConfiguration(context.Configuration.GetSection("NLog"));
					logging
						.ClearProviders()
						.AddNLog(context.Configuration)
						.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
				})
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
