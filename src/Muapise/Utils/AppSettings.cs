using System.Collections.Generic;

namespace Muapise.Utils
{
    public class AppSettings
    {
        /// <summary>When true enforces only Https access.</summary>
        public bool EnforceHttps { get; set; } = false;
        /// <summary>Enables/disables the Swagger API documentation.</summary>
        public bool EnableSwagger { get; set; } = false;

        public List<Dictionary<string,string>> WorkersPool { get; set; }
    }
}
