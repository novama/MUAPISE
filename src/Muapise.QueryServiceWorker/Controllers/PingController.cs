using System.Collections.Generic;
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
    public class PingController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<PingController> _logger;

        public PingController(ILogger<PingController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        [HttpGet("{host}")]
        public async Task<ActionResult<PingData>> Get(string host)
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
            _logger.LogDebug("Getting Ping data...");
            var pingData = await viewDnsRepository.GetPingData(ipAddress);
            _logger.LogDebug("Ping data result: {1}", pingData);

            var output = new PingData
            {
                Host = pingData.Query["host"],
                Replies = new List<PingData.ReplyData>()
            };
            foreach (var reply in pingData.Response.Replies)
                output.Replies.Add(new PingData.ReplyData("rtt", reply.Rtt));

            return Ok(output);
        }
    }
}