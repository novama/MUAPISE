using Microsoft.Extensions.Configuration;

namespace Muapise.Common.Config
{
    /// <summary>
    ///     Helper class used to read and load application configuration files.
    /// </summary>
    public static class AppConfigurationHelper
    {
        /// <summary>
        ///     Gets application configuration from a application settings Json file.
        /// </summary>
        /// <param name="appBasePathValue">
        ///     The application base path (or another path where the application settings file is
        ///     located)
        /// </param>
        /// <param name="appSettingsFileName">The application settings file name (Just name and extension).</param>
        /// <returns>A ConfigurationRoot object</returns>
        public static IConfigurationRoot GetAppConfiguration(string appBasePathValue,
            string appSettingsFileName = "appsettings.json")
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(appBasePathValue)
                .AddJsonFile(appSettingsFileName, false, false)
                .AddEnvironmentVariables()
                .Build();
            return config;
        }
    }
}