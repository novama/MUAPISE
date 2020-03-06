using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Muapise.Common.Domain.Models
{
    public class PingData
    {
        [JsonPropertyName("host")]
        public string Host { get; set; }

        [JsonPropertyName("replies")]
        public List<ReplyData> Replies { get; set; }

        public class ReplyData
        {
            public ReplyData()
            {
                //default constructor
            }

            public ReplyData(string key, string value)
            {
                Rtt = new Dictionary<string, string>();
                Rtt.Add(key, value);
            }

            [JsonPropertyName("rtt")]
            public Dictionary<string, string> Rtt { get; set; }
        }
    }
}
