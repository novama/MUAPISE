using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Muapise.QueryServiceWorker.Models
{
    public class ViewDnsReverseDnsResponse : ViewDnsResponseBase
    {
        [JsonPropertyName("response")]
        public Dictionary<string,string> Response { get; set; }

        public override string ToString()
        {
            return $"{nameof(Query)}: {Query}, {nameof(Response)}: {Response}";
        }
    }
}
