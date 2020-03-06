using System.Text.Json.Serialization;

namespace Muapise.Common.Domain.Models
{
    public class MuapiseResponse
    {
        [JsonPropertyName("ping")]
        public PingData PingData { get; set; }
        [JsonPropertyName("reverse_dns")]
        public ReverseDnsData ReverseDnsData { get; set; }
        [JsonPropertyName("geo_ip")]
        public GeoIpData GeoIpData { get; set; }

    }
}
