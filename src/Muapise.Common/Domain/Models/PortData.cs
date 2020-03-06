using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Muapise.Common.Domain.Models
{
    public class PortData
    {
        [JsonPropertyName("host")]
        public string Host { get; set; }

        [JsonPropertyName("ports")]
        public List<StatusData> Ports { get; set; }

        public class StatusData
        {
            public StatusData()
            {
                //default constructor
            }

            public StatusData(int number, string service, string status)
            {
                Number = number;
                Service = service;
                Status = status;
            }

            [JsonPropertyName("number")]
            public int Number { get; set; }
            [JsonPropertyName("service")]
            public string Service { get; set; }
            [JsonPropertyName("status")]
            public string Status { get; set; }
        }
    }
}
