using System.Text.Json.Serialization;

namespace Muapise.Common.Domain.Models
{
    public class ReverseDnsData
    {
        [JsonPropertyName("host")]
        public string Host { get; set; }

        [JsonPropertyName("rdns")]
        public string Rdns { get; set; }
    }
}
