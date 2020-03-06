using System.Text.Json.Serialization;

namespace Muapise.QueryServiceWorker.Models
{
    public class ViewDnsGeoIpResponse : ViewDnsResponseBase
    {
        [JsonPropertyName("response")]
        public ResponseData Response { get; set; }

        public class ResponseData
        {
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
            [JsonPropertyName("gmt_offset")]
            public string GmtOffset { get; set; }
            [JsonPropertyName("dst_offset")]
            public string DstOffset { get; set; }

            public override string ToString()
            {
                return $"{nameof(City)}: {City}, {nameof(CountryCode)}: {CountryCode}, {nameof(CountryName)}: {CountryName}, {nameof(Latitude)}: {Latitude}, {nameof(Longitude)}: {Longitude}, {nameof(RegionCode)}: {RegionCode}, {nameof(RegionName)}: {RegionName}, {nameof(ZipCode)}: {ZipCode}, {nameof(GmtOffset)}: {GmtOffset}, {nameof(DstOffset)}: {DstOffset}";
            }
        }

        public override string ToString()
        {
            return $"{nameof(Query)}: {Query}, {nameof(Response)}: {Response}";
        }
    }
}
