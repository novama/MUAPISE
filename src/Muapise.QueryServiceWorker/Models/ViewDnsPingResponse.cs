using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Muapise.QueryServiceWorker.Models
{
    public class ViewDnsPingResponse : ViewDnsResponseBase
    {
        [JsonPropertyName("response")]
        public ResponseData Response { get; set; }

        public class ResponseData
        {
            [JsonPropertyName("replys")]
            public IList<ReplyData> Replies { get; set; }

            public override string ToString()
            {
                return $"{nameof(Replies)}: {Replies}";
            }
        }

        public class ReplyData
        {
            [JsonPropertyName("rtt")]
            public string Rtt { get; set; }

            public override string ToString()
            {
                return $"{nameof(Rtt)}: {Rtt}";
            }
        }

        public override string ToString()
        {
            return $"{nameof(Query)}: {Query}, {nameof(Response)}: {Response}";
        }
    }
}
