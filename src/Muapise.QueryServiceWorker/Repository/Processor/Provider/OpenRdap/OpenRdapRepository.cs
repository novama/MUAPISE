using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Muapise.Common.Rest;

namespace Muapise.QueryServiceWorker.Repository.Processor.Provider.OpenRdap
{
    public class OpenRdapRepository
    {
        private readonly string _externalServiceUri = "https://www.rdap.net/";
        private readonly WebApiClientProcessor _apiProcessor;

        public OpenRdapRepository(HttpClient httpClient)
        {
            _apiProcessor = new WebApiClientProcessor(httpClient, _externalServiceUri);
        }

        public async Task<object> GetRdapData(string ipAddress)
        {
            var activityJson = await _apiProcessor.GetResponseContent("ip", ipAddress);
            object data = JsonSerializer.Deserialize(activityJson,null);
            return data;
        }
    }
}
