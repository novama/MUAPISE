using System.Text.Json.Serialization;

namespace Muapise.QueryServiceWorker.Models
{
    public class WhoisXmlApiDomainAvailabilityResponse
    {
        [JsonPropertyName("DomainInfo")]
        public DomainInfoData DomainInfo { get; set; }

        public class DomainInfoData
        {
            [JsonPropertyName("domainAvailability")]
            public string DomainAvailability { get; set; }
            [JsonPropertyName("domainName")]
            public string DomainName { get; set; }

            public override string ToString()
            {
                return $"{nameof(DomainAvailability)}: {DomainAvailability}, {nameof(DomainName)}: {DomainName}";
            }
        }

        public override string ToString()
        {
            return $"{nameof(DomainInfo)}: {DomainInfo}";
        }
    }
}
