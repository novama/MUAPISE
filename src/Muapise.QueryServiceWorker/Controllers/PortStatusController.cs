using System;
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
    public class PortStatusController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<PortStatusController> _logger;

        public PortStatusController(ILogger<PortStatusController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        [HttpGet("{host}")]
        public async Task<ActionResult<PortData>> Get(string host)
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
            _logger.LogDebug("Getting Port data...");
            var portData = await viewDnsRepository.GetPortStatusData(ipAddress);
            _logger.LogDebug("Port data result: {1}", portData);

            var output = new PortData
            {
                Host = portData.Query["host"],
                Ports = new List<PortData.StatusData>()
            };
            foreach (var status in portData.Response.Port)
                output.Ports.Add(new PortData.StatusData(Convert.ToInt32(status.Number), status.Service,
                    status.Status));

            return Ok(output);
        }
    }
}