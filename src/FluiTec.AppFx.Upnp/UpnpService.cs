using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Open.Nat;

namespace FluiTec.AppFx.Upnp
{
	/// <summary>	An upnp service. </summary>
	public class UpnpService
	{
		#region Fields

		private IPAddress _localIpAddress;

		#endregion

		#region Properties

		/// <summary>	The local IP address. </summary>
		public IPAddress LocalIpAddress => _localIpAddress ?? (_localIpAddress = GetLocalIpAddress());

		#endregion

		#region Methods

		/// <summary>	Checks if the given Tcp-PortMapping exists on the Upnp-RootDevice. </summary>
		/// <param name="mappingName">	Name of the mapping. </param>
		/// <param name="publicPort"> 	The public port. </param>
		/// <param name="ipAddress">  	(Optional) The IP address. If NULL - local ip will be assumed. </param>
		/// <returns>	Returns the PortMapping if available. </returns>
		public async Task<PortMapping> GetPortMapping(string mappingName, int publicPort, IPAddress ipAddress = null)
		{
			if (ipAddress == null) ipAddress = LocalIpAddress;

			var discoverer = new NatDiscoverer();
			var cts = new CancellationTokenSource(10000);
			var device = await discoverer.DiscoverDeviceAsync(PortMapper.Upnp, cts);
			var map = await device.GetSpecificMappingAsync(Protocol.Tcp, publicPort);

			var exists = map != null &&
			             mappingName == map.Description &&
			             map.PublicPort == publicPort &&
			             map.PrivateIP.Equals(ipAddress);
			if (!exists) return null;
			return new PortMapping
			{
				Name = map.Description,
				PublicAddress = map.PublicIP,
				PublicPort = map.PublicPort,
				PrivateAddress = map.PrivateIP,
				PrivatePort = map.PrivatePort
			};
		}

		/// <summary>	Adds a port mapping. </summary>
		/// <param name="applicationName">	Name of the application. </param>
		/// <param name="ipAddress">	  	(Optional) The IP address. If NULL - local ip will be
		/// 								assumed. </param>
		/// <returns>	A Task&lt;PortMapping&gt; </returns>
		public async Task<PortMapping> AddPortMapping(string applicationName, IPAddress ipAddress = null)
		{
			if (ipAddress == null) ipAddress = LocalIpAddress;

			var discoverer = new NatDiscoverer();
			var cts = new CancellationTokenSource(10000);
			var device = await discoverer.DiscoverDeviceAsync(PortMapper.Upnp, cts);
			var maps = await device.GetAllMappingsAsync();

			var mapList = maps as IList<Mapping> ?? maps.ToList();
			var maxPortPublic = mapList.Select(map => map.PublicPort).Max();
			var maxPortPrivate = mapList.Select(map => map.PrivatePort).Max();

			var newMap =
				device.CreatePortMapAsync(new Mapping(Protocol.Tcp, ipAddress, maxPortPrivate + 1, maxPortPublic + 1, int.MaxValue,
					$"{applicationName}"));

			return await GetPortMapping(applicationName, maxPortPublic + 1, ipAddress);
		}

		/// <summary>	Gets IP addres. </summary>
		/// <returns>	The IP addres. </returns>
		private static IPAddress GetLocalIpAddress()
		{
			using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
			{
				socket.Connect(host: "8.8.8.8", port: 65530);
				var endPoint = socket.LocalEndPoint as IPEndPoint;
				return endPoint?.Address;
			}
		}

		#endregion
	}
}
