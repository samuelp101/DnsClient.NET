﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace DnsClient2
{
    public class NameServer
    {
        /// <summary>
        /// The default DNS server port.
        /// </summary>
        public const int DefaultPort = 53;

        /// <summary>
        /// Gets a list of name servers by iterating over the available network interfaces.
        /// </summary>
        /// <returns>The list of name servers.</returns>
        public static ICollection<DnsEndPoint> ResolveNameServers()
        {
            var result = new HashSet<DnsEndPoint>();

            // TODO: check filter loopback adapters and such? Getting unsupported exceptions when running a query against those ip6 DNS addresses.
            var adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface networkInterface in adapters.Where(p=>p.OperationalStatus == OperationalStatus.Up))
            {
                foreach (IPAddress dnsAddress in networkInterface.GetIPProperties().DnsAddresses)
                {
                    result.Add(new DnsEndPoint(dnsAddress.ToString(), DefaultPort));
                }
            }

            return result.ToArray();
        }
    }
}