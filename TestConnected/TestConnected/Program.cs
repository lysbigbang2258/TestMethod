namespace TestConnected
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        /// <summary>
        /// The main.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        static void Main(string[] args) {
            IPEndPoint ip = new IPEndPoint(IPAddress.Parse("192.168.172.1"), 8974);
            IPEndPoint ip2 = new IPEndPoint(IPAddress.Parse("192.168.172.1"), 8975);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            socket.Bind(ip);
            Thread.Sleep(100);
            bool result = IsPointBlocked(ip2);
            Console.WriteLine(result);
            Console.ReadKey();
        }

        /// <summary>
        /// The is point blocked.
        /// </summary>
        /// <param name="ip">
        /// The ip.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        static bool IsPointBlocked(IPEndPoint ip)
        {
            IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
            var ipendpoints = properties.GetActiveUdpListeners();
            return ipendpoints.Any(endpoint => endpoint.Port == ip.Port);
        }
    }
}
