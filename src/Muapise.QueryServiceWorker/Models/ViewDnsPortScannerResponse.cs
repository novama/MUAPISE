using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Muapise.QueryServiceWorker.Models
{
    public class ViewDnsPortScannerResponse : ViewDnsResponseBase
    {
        [JsonPropertyName("response")]
        public ViewDnsPortData Response { get; set; }

        public class ViewDnsPortData
        {
            [JsonPropertyName("port")]
            public List<ViewDnsPortDetailsData> Port { get; set; }

            public override string ToString()
            {
                return $"{nameof(Port)}: {Port}";
            }
        }

        public class ViewDnsPortDetailsData
        {
            [JsonPropertyName("number")]
            public string Number { get; set; }
            [JsonPropertyName("service")]
            public string Service { get; set; }
            [JsonPropertyName("status")]
            public string Status { get; set; }

            public override string ToString()
            {
                return $"{nameof(Number)}: {Number}, {nameof(Service)}: {Service}, {nameof(Status)}: {Status}";
            }
        }

        public override string ToString()
        {
            return $"{nameof(Query)}: {Query}, {nameof(Response)}: {Response}";
        }
    }
}
