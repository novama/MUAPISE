using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Muapise.Common.Domain.Models;
using Muapise.Common.Utils;
using Muapise.Repository.Processor;
using Muapise.Utils;

namespace Muapise.Controllers
{
    [ApiController]
    [Route(ApiInfo.DefaultApiRoute)]
    public class HostDataController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<HostDataController> _logger;

        public HostDataController(ILogger<HostDataController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        [HttpGet("{host}/{selectedServices}")]
        public async Task<ActionResult<GeoIpData>> Get(string host, string selectedServices = null)
        {
            var errorMsg = "Invalid host or host could not be reached";
            if (string.IsNullOrEmpty(host))
            {
                _logger.LogError(errorMsg);
                return BadRequest(errorMsg);
            }

            var validatedIp = NetUtils.GetIpAddress(host);
            if (validatedIp == null)
            {
                var msg = "Invalid host or host could not be reached";
                _logger.LogError(msg);
                return BadRequest(msg);
            }

            var services = selectedServices.Split('|');
            if (services == null || services.Count() == 0) services = MuapiseServices.DefaultList;

            services = services.Distinct().ToArray();

            var ipAddress = validatedIp.ToString();
            var client = _clientFactory.CreateClient();

            var finalOutput = new Dictionary<string, object>();

            var queryServiceWorkerRepository = new QueryServiceWorkerRepository(client);
            foreach (var service in services)
                switch (service.ToLower())
                {
                    case MuapiseServices.AvailableServices.Ping:
                    {
                        var pingData = await queryServiceWorkerRepository.GetPingData(ipAddress,
                            WorkerEndPoints.Instance.GetFirstEndPoint() + ApiInfo.ApiWorkerSegment);
                        finalOutput.Add(MuapiseServices.AvailableServices.Ping, pingData);
                        break;
                    }
                    case MuapiseServices.AvailableServices.GeoIp:
                    {
                        var geoIpData = await queryServiceWorkerRepository.GetGeoIpData(ipAddress,
                            WorkerEndPoints.Instance.GetFirstEndPoint() + ApiInfo.ApiWorkerSegment);
                        finalOutput.Add(MuapiseServices.AvailableServices.GeoIp, geoIpData);
                        break;
                    }
                    case MuapiseServices.AvailableServices.ReverseDns:
                    {
                        var reverseDnsData = await queryServiceWorkerRepository.GetReverseDnsData(ipAddress,
                            WorkerEndPoints.Instance.GetFirstEndPoint() + ApiInfo.ApiWorkerSegment);
                        finalOutput.Add(MuapiseServices.AvailableServices.ReverseDns, reverseDnsData);
                        break;
                    }
                    case MuapiseServices.AvailableServices.PortStatus:
                    {
                        var portData = await queryServiceWorkerRepository.GetPortData(ipAddress,
                            WorkerEndPoints.Instance.GetFirstEndPoint() + ApiInfo.ApiWorkerSegment);
                        finalOutput.Add(MuapiseServices.AvailableServices.PortStatus, portData);
                        break;
                    }
                }


            return Ok(finalOutput);
        }
    }
}