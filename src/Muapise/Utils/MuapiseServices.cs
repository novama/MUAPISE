using Muapise.Common.Domain.Models;

namespace Muapise.Utils
{
    public static class MuapiseServices
    {
        public static readonly string[] DefaultList =
        {
            AvailableServices.Ping, AvailableServices.GeoIp
        };

        public static class AvailableServices
        {
            public const string GeoIp = "geoip";
            public const string ReverseDns = "reversedns";
            public const string Ping = "ping";
            public const string PortStatus = "portstatus";
        }
    }
}