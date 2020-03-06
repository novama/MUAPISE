using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Muapise.QueryServiceWorker.Models
{
    public class ViewDnsResponseBase
    {
        [JsonPropertyName("query")]
        public Dictionary<string,string> Query { get; set; }
    }
}
