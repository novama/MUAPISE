using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Muapise.Common.Domain.Models;
using Muapise.Common.Utils;
using Muapise.QueryServiceWorker.Repository.Processor.Provider.ViewDns;
using Muapise.QueryServiceWorker.Utils;

namespace Muapise.QueryServiceWorker.Controllers
{
    [ApiController]
    [Route(ApiInfo.DefaultApiRoute)]
    public class ReverseDnsController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<ReverseDnsController> _logger;

        public ReverseDnsController(ILogger<ReverseDnsController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        [HttpGet("{host}")]
        public async Task<ActionResult<ReverseDnsData>> Get(string host)
        {
            var validatedIp = NetUtils.GetIpAddress(host);
            if (validatedIp == null)
            {
                var msg = "Invalid host or host could not be reached";
                _logger.LogError(msg);
                return BadRequest(msg);
            }

            var ipAddress = validatedIp.ToString();
            var client = _clientFactory.CreateClient();

            var viewDnsRepository = new ViewDnsRepository(client);
            _logger.LogDebug("Getting Reverse DNS data...");
            var reverseDnsData = await viewDnsRepository.GetReverseDnsData(ipAddress);
            _logger.LogDebug("Reverse DNS data result: {1}", reverseDnsData);

            var output = new ReverseDnsData
            {
                Host = reverseDnsData.Query["ip"],
                Rdns = reverseDnsData.Response["rdns"]
            };

            return Ok(output);
        }
    }
}