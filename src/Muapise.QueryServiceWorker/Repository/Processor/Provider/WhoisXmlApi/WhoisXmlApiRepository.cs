using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Muapise.Common.Rest;
using Muapise.QueryServiceWorker.Models;

namespace Muapise.QueryServiceWorker.Repository.Processor.Provider.WhoisXmlApi
{
    public class WhoisXmlApiRepository
    {
        private readonly WebApiClientProcessor _apiProcessor;
        private readonly string _externalServiceUri = "https://domain-availability-api.whoisxmlapi.com/api/v1";

        public WhoisXmlApiRepository(HttpClient httpClient)
        {
            _apiProcessor = new WebApiClientProcessor(httpClient, _externalServiceUri);
        }

        public static string ApiKey { get; set; } = "at_vg2gtR1ccqdyReKVZW6J6S3PgbHUh";

        /// <summary>
        ///     Checks whether a domain name is available for registration.
        /// </summary>
        /// <param name="domainName">The domain name to check for availability.</param>
        /// <returns></returns>
        public async Task<WhoisXmlApiDomainAvailabilityResponse> GetDomainAvailabilityData(string domainName)
        {
            var parameters = new StringBuilder();
            parameters.Append("?domainName=" + domainName);
            parameters.Append("&apikey=" + ApiKey);
            parameters.Append("&outputFormat=json");

            var activityJson = await _apiProcessor.GetResponseContent("", parameters.ToString());
            var data = JsonSerializer.Deserialize<WhoisXmlApiDomainAvailabilityResponse>(activityJson);
            return data;
        }
    }
}