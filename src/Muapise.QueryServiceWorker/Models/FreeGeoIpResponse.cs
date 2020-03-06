using System.Text.Json.Serialization;

namespace Muapise.QueryServiceWorker.Models
{
    public class FreeGeoIpResponse
    {
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }
        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }
        [JsonPropertyName("ip")]
        public string Ipv4 { get; set; }
        [JsonPropertyName("latitude")]
        public decimal Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public decimal Longitude { get; set; }
        [JsonPropertyName("metro_code")]
        public int MetroCode { get; set; }
        [JsonPropertyName("region_code")]
        public string RegionCode { get; set; }
        [JsonPropertyName("region_name")]
        public string RegionName { get; set; }
        [JsonPropertyName("time_zone")]
        public string TimeZone { get; set; }
        [JsonPropertyName("zip_code")]
        public string ZipCode { get; set; }

        public override string ToString()
        {
            return $"{nameof(City)}: {City}, {nameof(CountryCode)}: {CountryCode}, {nameof(CountryName)}: {CountryName}, {nameof(Ipv4)}: {Ipv4}, {nameof(Latitude)}: {Latitude}, {nameof(Longitude)}: {Longitude}, {nameof(MetroCode)}: {MetroCode}, {nameof(RegionCode)}: {RegionCode}, {nameof(RegionName)}: {RegionName}, {nameof(TimeZone)}: {TimeZone}, {nameof(ZipCode)}: {ZipCode}";
        }
    }
}