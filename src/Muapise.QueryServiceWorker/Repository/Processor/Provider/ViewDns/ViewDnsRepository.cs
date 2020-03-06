using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Muapise.Common.Rest;
using Muapise.QueryServiceWorker.Models;

namespace Muapise.QueryServiceWorker.Repository.Processor.Provider.ViewDns
{
    public class ViewDnsRepository
    {
        private readonly WebApiClientProcessor _apiProcessor;
        private readonly string _externalServiceUri = "https://api.viewdns.info/";

        public ViewDnsRepository(HttpClient httpClient)
        {
            _apiProcessor = new WebApiClientProcessor(httpClient, _externalServiceUri);
        }

        public static string ApiKey { get; set; } = "b47b0cfaba23eb2544f9a25f8cf562efac4ecc9d";

        /// <summary>
        ///     Find the reverse DNS entry (PTR) for a given IP. This is generally the server or host name.
        /// </summary>
        /// <param name="ipAddress">The IP address to retrieve the reverse DNS record for.</param>
        /// <returns></returns>
        public async Task<ViewDnsReverseDnsResponse> GetReverseDnsData(string ipAddress)
        {
            var parameters = new StringBuilder();
            parameters.Append("?ip=" + ipAddress);
            parameters.Append("&apikey=" + ApiKey);
            parameters.Append("&output=json");

            var activityJson = await _apiProcessor.GetResponseContent("reversedns", parameters.ToString());
            var data = JsonSerializer.Deserialize<ViewDnsReverseDnsResponse>(activityJson);
            return data;
        }

        /// <summary>
        ///     Displays geographic information about a supplied IP address including city, country, latitude, longitude and more.
        /// </summary>
        /// <param name="ipAddress">The ip address to find the location of.</param>
        /// <returns></returns>
        public async Task<ViewDnsGeoIpResponse> GetGeoIpData(string ipAddress)
        {
            var parameters = new StringBuilder();
            parameters.Append("?ip=" + ipAddress);
            parameters.Append("&apikey=" + ApiKey);
            parameters.Append("&output=json");

            var activityJson = await _apiProcessor.GetResponseContent("iplocation", parameters.ToString());
            var data = JsonSerializer.Deserialize<ViewDnsGeoIpResponse>(activityJson);
            return data;
        }

        /// <summary>
        ///     Tests how long a response from remote system takes to reach the ViewDNS server. Useful for detecting latency issues
        ///     on network connections.
        /// </summary>
        /// <param name="host">The domain or IP address to perform a ping on.</param>
        /// <returns></returns>
        public async Task<ViewDnsPingResponse> GetPingData(string host)
        {
            var parameters = new StringBuilder();
            parameters.Append("?host=" + host);
            parameters.Append("&apikey=" + ApiKey);
            parameters.Append("&output=json");

            var activityJson = await _apiProcessor.GetResponseContent("ping", parameters.ToString());
            var data = JsonSerializer.Deserialize<ViewDnsPingResponse>(activityJson);
            return data;
        }

        /// <summary>
        ///     Tests how long a response from remote system takes to reach the ViewDNS server. Useful for detecting latency issues
        ///     on network connections.
        /// </summary>
        /// <param name="host">The domain or IP address to perform a ping on.</param>
        /// <returns></returns>
        public async Task<ViewDnsResponseBase> GetRdapData(string host)
        {
            var parameters = new StringBuilder();
            parameters.Append("?host=" + host);
            parameters.Append("&apikey=" + ApiKey);
            parameters.Append("&output=json");

            var activityJson = await _apiProcessor.GetResponseContent("ping", parameters.ToString());
            var data = JsonSerializer.Deserialize<ViewDnsResponseBase>(activityJson);
            return data;
        }

        /// <summary>
        ///     Tests whether common ports are open on a host.
        ///     Ports scanned are: 21, 22, 23, 25, 80, 110, 139, 143, 445, 1433, 1521, 3306 and 3389
        /// </summary>
        /// <param name="host">The domain or IP address to perform a ping on.</param>
        /// <returns></returns>
        public async Task<ViewDnsPortScannerResponse> GetPortStatusData(string host)
        {
            var parameters = new StringBuilder();
            parameters.Append("?host=" + host);
            parameters.Append("&apikey=" + ApiKey);
            parameters.Append("&output=json");

            var activityJson = await _apiProcessor.GetResponseContent("portscan", parameters.ToString());
            var data = JsonSerializer.Deserialize<ViewDnsPortScannerResponse>(activityJson);
            return data;
        }
    }
}