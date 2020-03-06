using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Muapise.Common.Domain.Models;
using Muapise.Common.Utils;
using Muapise.QueryServiceWorker.Repository.Processor.Provider.FreeGeoIp;
using Muapise.QueryServiceWorker.Utils;

namespace Muapise.QueryServiceWorker.Controllers
{
    [ApiController]
    [Route(ApiInfo.DefaultApiRoute)]
    public class GeoIpLocationController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<GeoIpLocationController> _logger;

        public GeoIpLocationController(ILogger<GeoIpLocationController> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        [HttpGet("{host}")]
        public async Task<ActionResult<GeoIpData>> Get(string host)
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

            var freeGeoIpRepository = new FreeGeoIpRepository(client);
            _logger.LogDebug("Getting GeoIp data...");
            var geoIpData = await freeGeoIpRepository.GetGeoIpData(ipAddress);
            _logger.LogDebug("GeoIp data result: {1}", geoIpData);

            var output = new GeoIpData
            {
                City = geoIpData.City,
                CountryName = geoIpData.CountryName,
                CountryCode = geoIpData.CountryCode,
                Host = geoIpData.Ipv4,
                RegionName = geoIpData.RegionName,
                RegionCode = geoIpData.RegionCode,
                Latitude = geoIpData.Latitude,
                Longitude = geoIpData.Longitude,
                ZipCode = geoIpData.ZipCode
            };

            return Ok(output);
        }
    }
}