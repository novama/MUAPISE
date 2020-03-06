namespace Muapise.QueryServiceWorker.Utils
{
    public class AppSettings
    {
        /// <summary>When true enforces only Https access.</summary>
        public bool EnforceHttps { get; set; } = false;
        /// <summary>Enables/disables the Swagger API documentation.</summary>
        public bool EnableSwagger { get; set; } = false;
    }
}
