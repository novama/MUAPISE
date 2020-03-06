using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Muapise.Common.Domain.Models;
using Muapise.Common.Utils;
using Muapise.QueryServiceWorker.Repository.Processor.Provider.FreeGeoIp;
using Muapise.QueryServiceWorker.Repository.Processor.Provider.OpenRdap;
using Muapise.QueryServiceWorker.Repository.Processor.Provider.ViewDns;
using Muapise.QueryServiceWorker.Repository.Processor.Provider.WhoisXmlApi;
using Muapise.QueryServiceWorker.Utils;

namespace Muapise.QueryServiceWorker.Controllers
{
    [ApiController]
    [Route(ApiInfo.DefaultApiRoute)]
    public class RdapController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<RdapController> _logger;

        public RdapController(ILogger<RdapController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        [HttpGet("{host}")]
        //TODO fix data type
        public async Task<ActionResult<object>> Get(string host)
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

            var openRdapRepository = new OpenRdapRepository(client);
            _logger.LogDebug("Getting RDAP data...");
            var rdapData = await openRdapRepository.GetRdapData(ipAddress);
            _logger.LogDebug("RDAP data retrieved.");

           //TODO RDAP map


            return Ok(rdapData);
        }
    }
}