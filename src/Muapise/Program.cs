using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Muapise.Common.Config;
using Muapise.Utils;
using NLog;
using NLog.Web;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Muapise
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configuring logging
            NLogConfigHelper.ConfigureLogging(AppInfo.AppName, AppInfo.AppBasePath);
            var logger = NLogBuilder.ConfigureNLog(LogManager.Configuration).GetCurrentClassLogger();

            try
            {
                logger.Info("Application Started.");
                // Start web host
                BuildWebHost(args).Run();
                // Log application termination
                logger.Info("Application Terminated.");
            }
            catch (Exception ex)
            {
                // Log the exception and terminates the execution
                logger.Fatal(ex, "Application execution terminated unexpectedly.");
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
                Environment.Exit(-1);
            }
        }

        private static IWebHost BuildWebHost(string[] args, IConfigurationRoot configuration = null)
        {
            if (configuration == null) configuration = AppConfigurationHelper.GetAppConfiguration(AppInfo.AppBasePath);

            return WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(configuration)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                })
                // NLog: Setup NLog for Dependency injection
                .UseNLog()
                .Build();
        }
    }
}