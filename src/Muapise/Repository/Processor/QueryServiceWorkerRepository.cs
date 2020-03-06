using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Muapise.Common.Domain.Models;
using Muapise.Common.Rest;

namespace Muapise.Repository.Processor
{
    public class QueryServiceWorkerRepository
    {
        private readonly WebApiClientProcessor _apiProcessor;

        public QueryServiceWorkerRepository(HttpClient httpClient)
        {
            _apiProcessor = new WebApiClientProcessor(httpClient, null);
        }

        /// <summary>
        ///     Displays geographic information about a supplied IP address including city, country, latitude, longitude and more.
        /// </summary>
        /// <param name="host">The host address to find the location of.</param>
        /// <param name="externalServiceUri">API uri</param>
        /// <returns></returns>
        public async Task<GeoIpData> GetGeoIpData(string host, string externalServiceUri)
        {
            var activityJson = await _apiProcessor.GetResponseContent(externalServiceUri, "geoiplocation", host);
            var data = JsonSerializer.Deserialize<GeoIpData>(activityJson);
            return data;
        }

        /// <summary>
        ///     Tests how long a response from remote system takes to reach the server.
        /// </summary>
        /// <param name="host">The host address to find the location of.</param>
        /// <param name="externalServiceUri">API uri</param>
        /// <returns></returns>
        public async Task<PingData> GetPingData(string host, string externalServiceUri)
        {
            var activityJson = await _apiProcessor.GetResponseContent(externalServiceUri, "ping", host);
            var data = JsonSerializer.Deserialize<PingData>(activityJson);
            return data;
        }

        /// <summary>
        ///     Find the reverse DNS entry (PTR) for a given IP.
        /// </summary>
        /// <param name="host">The host address to find the location of.</param>
        /// <param name="externalServiceUri">API uri</param>
        /// <returns></returns>
        public async Task<ReverseDnsData> GetReverseDnsData(string host, string externalServiceUri)
        {
            var activityJson = await _apiProcessor.GetResponseContent(externalServiceUri, "reversedns", host);
            var data = JsonSerializer.Deserialize<ReverseDnsData>(activityJson);
            return data;
        }

        /// <summary>
        ///     Tests whether common ports are open on a host.
        /// </summary>
        /// <param name="host">The domain or IP address to perform a ping on.</param>
        /// <param name="externalServiceUri">API uri</param>
        /// <returns></returns>
        public async Task<PortData> GetPortData(string host, string externalServiceUri)
        {
            var activityJson = await _apiProcessor.GetResponseContent(externalServiceUri, "portstatus", host);
            var data = JsonSerializer.Deserialize<PortData>(activityJson);
            return data;
        }
    }
}