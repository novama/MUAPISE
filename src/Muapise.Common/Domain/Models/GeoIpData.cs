using System.Text.Json.Serialization;

namespace Muapise.Common.Domain.Models
{
    public class GeoIpData
    {
        [JsonPropertyName("host")]
        public string Host { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }
        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }
        [JsonPropertyName("latitude")]
        public decimal Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public decimal Longitude { get; set; }
        [JsonPropertyName("region_code")]
        public string RegionCode { get; set; }
        [JsonPropertyName("region_name")]
        public string RegionName { get; set; }
        [JsonPropertyName("zipcode")]
        public string ZipCode { get; set; }
    }
}