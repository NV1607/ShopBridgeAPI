using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using ShopBridge.Utility;

namespace ShopBridge
{
    /// <summary>
    /// Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// CreateHostBuilder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            //Configure Custom File Logger to generate txt log files
            //For logging Information, Debug and Errors
            .ConfigureLogging((hostBuilderContext, logging) =>
            {
                logging.AddFileLogger(options =>
                {
                    hostBuilderContext.Configuration
                    .GetSection("Logging")
                    .GetSection("FileLogger")
                    .GetSection("Options")
                    .Bind(options);
                });
            });
    }
}
