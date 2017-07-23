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
		#region Const

		/// <summary>	The upnp timeout. </summary>
		private const int UpnpTimeout = 3000;

		#endregion

		#region Fields

		private IPAddress _localIpAddress;

		#endregion

		#region Properties

		/// <summary>	The local IP address. </summary>
		public IPAddress LocalIpAddress => _localIpAddress ?? (_localIpAddress = GetLocalIpAddress());

		#endregion

		#region Methods

		/// <summary>	Checks if the given Tcp-PortMapping exists on the Upnp-RootDevice. </summary>
		/// <param name="privatePort"></param>
		/// <param name="ipAddress">
		///     (Optional) The IP address. If NULL - local ip will be
		///     assumed.
		/// </param>
		/// <returns>	Returns the PortMapping if available. </returns>
		public async Task<PortMapping> GetPortMapping(int privatePort, IPAddress ipAddress = null)
		{
			try
			{
				if (ipAddress == null) ipAddress = LocalIpAddress;

				var discoverer = new NatDiscoverer();
				var cts = new CancellationTokenSource(UpnpTimeout);
				var device = await discoverer.DiscoverDeviceAsync(PortMapper.Upnp, cts);
				var all = await device.GetAllMappingsAsync();
				var map = all.SingleOrDefault(m =>
					m.PrivatePort == privatePort &&
					m.PrivateIP.Equals(ipAddress));

				var exists = map != null &&
				             map.PrivatePort == privatePort &&
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
			catch (NatDeviceNotFoundException e)
			{
				throw new UpnpException(ExceptionMessages.DeviceNotFound, e);
			}
		}

		/// <summary>	Removes the port mapping. </summary>
		/// <exception cref="UpnpException">	Thrown when an Upnp error condition occurs. </exception>
		/// <param name="privatePort">	  	The private port. </param>
		/// <param name="ipAddress">
		///     (Optional) The IP address. If NULL - local ip will be
		///     assumed.
		/// </param>
		/// <returns>	A Task. </returns>
		public async Task RemovePortMapping(int privatePort, IPAddress ipAddress = null)
		{
			try
			{
				if (ipAddress == null) ipAddress = LocalIpAddress;

				var discoverer = new NatDiscoverer();
				var cts = new CancellationTokenSource(UpnpTimeout);
				var device = await discoverer.DiscoverDeviceAsync(PortMapper.Upnp, cts);

				var mapping = await GetPortMapping(privatePort, ipAddress);

				if (mapping != null)
					await device.DeletePortMapAsync(new Mapping(Protocol.Tcp, IPAddress.Parse(mapping.PrivatecIpAddress),
						mapping.PrivatePort, mapping.PublicPort, int.MaxValue, mapping.Name));
			}
			catch (NatDeviceNotFoundException e)
			{
				throw new UpnpException(ExceptionMessages.DeviceNotFound, e);
			}
		}

		/// <summary>	Adds a port mapping. </summary>
		/// <param name="applicationName">	Name of the application. </param>
		/// <param name="privatePort">	  	The private port. </param>
		/// <param name="ipAddress">
		///     (Optional) The IP address. If NULL - local ip will be
		///     assumed.
		/// </param>
		/// <returns>	A Task&lt;PortMapping&gt; </returns>
		public async Task<PortMapping> AddPortMapping(string applicationName, int privatePort, IPAddress ipAddress = null)
		{
			try
			{
				if (ipAddress == null) ipAddress = LocalIpAddress;

				var discoverer = new NatDiscoverer();
				var cts = new CancellationTokenSource(UpnpTimeout);
				var device = await discoverer.DiscoverDeviceAsync(PortMapper.Upnp, cts);
				var maps = await device.GetAllMappingsAsync();

				var enumeratedMaps = maps as IList<Mapping> ?? maps.ToList();
				var mapList = maps as IList<Mapping> ?? enumeratedMaps.ToList();
				var maxPortPublic = mapList.Select(map => map.PublicPort).Max();

				var exists = enumeratedMaps.SingleOrDefault(map =>
					map.PrivatePort == privatePort &&
					map.PrivateIP.Equals(ipAddress));

				if (exists != null)
					await RemovePortMapping(exists.PrivatePort, ipAddress);

				await device.CreatePortMapAsync(new Mapping(Protocol.Tcp, ipAddress, privatePort, maxPortPublic + 1, int.MaxValue,
					$"{applicationName}_{maxPortPublic + 1}"));

				return await GetPortMapping(privatePort, ipAddress);
			}
			catch (NatDeviceNotFoundException e)
			{
				throw new UpnpException(ExceptionMessages.DeviceNotFound, e);
			}
		}

		/// <summary>	Gets IP addres. </summary>
		/// <returns>	The IP addres. </returns>
		private static IPAddress GetLocalIpAddress()
		{
			using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, protocolType: 0))
			{
				socket.Connect(host: "8.8.8.8", port: 65530);
				var endPoint = socket.LocalEndPoint as IPEndPoint;
				return endPoint?.Address;
			}
		}

		#endregion
	}
}