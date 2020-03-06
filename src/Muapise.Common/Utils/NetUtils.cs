using System;
using System.Net;
using System.Net.Sockets;

namespace Muapise.Common.Utils
{
    public static class NetUtils
    {
        /// <summary>
        ///     Gets the Ipv4 address object for a given host or address.
        /// </summary>
        /// <param name="hostNameOrAddresses">The host name or addresses.</param>
        /// <returns>
        ///     An IPAddress object containing the IP address from the given host;
        ///     empty if host could not be found.
        /// </returns>
        public static IPAddress GetIpAddress(string hostNameOrAddresses)
        {
            try
            {
                foreach (var ipAddress in Dns.GetHostAddresses(hostNameOrAddresses))
                    if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                        return ipAddress;
            }
            catch (Exception)
            {
                //The handle of this exception is not relevant for the function
            }
            return null;
        }
 
        /// <summary>
        ///     Checks if the given address is a valid IP address, and returns a boolean value.
        /// </summary>
        /// <param name="ipAddress">IP address of to check</param>
        /// <returns>True if the input value is a valid IP Address</returns>
        public static bool IsValidIpAddress(string ipAddress)
        {
            var result = false;
            try
            {
                if (!ipAddress.IsNaturalNumber())
                {
                    IPAddress.Parse(ipAddress);
                    result = true;
                }
            }
            catch
            {
                //The handle of this exception is not relevant for the function
            }
            return result;
        }
    }
}
