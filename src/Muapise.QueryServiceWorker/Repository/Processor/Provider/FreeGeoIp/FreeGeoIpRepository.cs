using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Muapise.Common.Rest;
using Muapise.QueryServiceWorker.Models;

namespace Muapise.QueryServiceWorker.Repository.Processor.Provider.FreeGeoIp
{
    public class FreeGeoIpRepository
    {
        private readonly string _externalServiceUri = "https://freegeoip.app/";
        private readonly WebApiClientProcessor _apiProcessor;

        public FreeGeoIpRepository(HttpClient httpClient)
        {
            _apiProcessor = new WebApiClientProcessor(httpClient, _externalServiceUri);
        }

        /// <summary>
        /// Displays geographic information about a supplied IP address including city, country, latitude, longitude and more.
        /// </summary>
        /// <param name="ipAddress">The ip address to find the location of.</param>
        /// <returns></returns>
        public async Task<FreeGeoIpResponse> GetGeoIpData(string ipAddress)
        {
            var activityJson = await _apiProcessor.GetResponseContent("json", ipAddress);
            var data = JsonSerializer.Deserialize<FreeGeoIpResponse>(activityJson);
            return data;
        }

    }
}
