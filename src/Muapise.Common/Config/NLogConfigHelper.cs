using System;
using Microsoft.Extensions.Configuration;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;

namespace Muapise.Common.Config
{
    /// <summary>
    ///     Helper class used to easily configure NLog with the configuration found in application settings files.
    /// </summary>
    public static class NLogConfigHelper
    {
        public const string AppBasePathEnvironmentVariableName = "APPBASEPATH";
        public const string AppNameEnvironmentVariableName = "APPNAME";
        public const string NLogSectionName = "NLog";

        /// <summary>
        ///     Loads and returns the NLog configuration parameters from the given application settings file name.
        ///     Both arguments are used also as the values for the environment variables 'APPBASEPATH' and 'APPNAME'
        ///     that can be used into the application settings file.
        /// </summary>
        /// <param name="appNameValue">The application name.</param>
        /// <param name="appBasePathValue">
        ///     The application base path (or another path where the application settings file is
        ///     located)
        /// </param>
        /// <param name="appSettingsFileName">The application settings file name (Just name and extension).</param>
        /// <returns>A ConfigurationSection object.</returns>
        public static IConfigurationSection LoadConfigFromAppSettings(string appNameValue, string appBasePathValue,
            string appSettingsFileName = "appsettings.json")
        {
            // Get application configuration
            var configuration = AppConfigurationHelper.GetAppConfiguration(appBasePathValue, appSettingsFileName);

            Environment.SetEnvironmentVariable(AppBasePathEnvironmentVariableName,
                appBasePathValue);
            Environment.SetEnvironmentVariable(AppNameEnvironmentVariableName,
                appNameValue);

            var logConfigDetails = configuration.GetSection(NLogSectionName);
            return logConfigDetails;
        }

        /// <summary>
        ///     Configures NLog LogManager with the settings loaded from a given application settings file.
        /// </summary>
        /// <param name="appNameValue">The application name.</param>
        /// <param name="appBasePathValue">
        ///     The application base path (or another path where the application settings file is
        ///     located)
        /// </param>
        /// <param name="appSettingsFileName">The application settings file name (Just name and extension).</param>
        public static void ConfigureLogging(string appNameValue, string appBasePathValue,
            string appSettingsFileName = "appsettings.json")
        {
            try
            {
                // Initialize logger
                LogManager.Configuration =
                    new NLogLoggingConfiguration(LoadConfigFromAppSettings(appNameValue, appBasePathValue,
                        appSettingsFileName));
                var logger = NLogBuilder.ConfigureNLog(LogManager.Configuration).GetCurrentClassLogger();
                // Log application start up
                logger.Info("Logging configuration succesfully loaded.");
            }
            catch (Exception ex)
            {
                // As there is not logger configured, display the exception message
                // in console and terminates the execution
                Console.WriteLine(ex.Message);
                Console.WriteLine("Application execution terminated unexpectedly when configuring logging.");
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
                Environment.Exit(-1);
            }
        }
    }
}