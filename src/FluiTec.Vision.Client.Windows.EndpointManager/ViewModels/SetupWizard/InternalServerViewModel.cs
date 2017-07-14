using System;
using System.Linq;
using System.Net.NetworkInformation;
using FluiTec.Vision.Client.Windows.EndpointManager.Resources.Localization.Views.Setup.Wizard;
using FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.Wizard;
using FluiTec.Vision.Client.Windows.EndpointManager.Views.SetupWizard;

namespace FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.SetupWizard
{
	/// <summary>	A ViewModel for the internal server. </summary>
	public class InternalServerViewModel : WizardPageViewModel
	{
		#region Fields

		/// <summary>	The local port. </summary>
		private int _localPort;

		#endregion

		#region Constructors

		public InternalServerViewModel()
		{
			Title = InternalServer.Header;
			Description = InternalServer.Description;
			Content = new InternalServerPage();

			LocalPort = GetFreePortInRange(MIN_PORT, MAX_PORT);
		}

		#endregion

		#region Properties

		/// <summary>	Gets or sets the local port. </summary>
		/// <value>	The local port. </value>
		public int LocalPort
		{
			get => _localPort;
			set
			{
				_localPort = value;
				Validate();
				OnPropertyChanged();
			}
		}

		#endregion

		#region Methods

		/// <summary>	Validates the model. </summary>
		/// <returns>	True if it succeeds, false if it fails. </returns>
		protected override bool ValidateModel()
		{
			return new[]
			{
				LocalPort > 0
			}.Any(b => b);
		}

		#endregion

		#region Methods

		/// <summary>	Finds the freeportinrange of the given arguments. </summary>
		/// <exception cref="ApplicationException"> Thrown when an Application error condition occurs. </exception>
		/// <param name="portStartIndex">	Zero-based index of the port start. </param>
		/// <param name="portEndIndex">  	Zero-based index of the port end. </param>
		/// <returns>	The calculated free port in range. </returns>
		private static int GetFreePortInRange(int portStartIndex, int portEndIndex)
		{
			var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

			var tcpEndPoints = ipGlobalProperties.GetActiveTcpListeners();
			var usedServerTCpPorts = tcpEndPoints.Select(p => p.Port).ToList();

			var udpEndPoints = ipGlobalProperties.GetActiveUdpListeners();
			var usedServerUdpPorts = udpEndPoints.Select(p => p.Port).ToList();

			var tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();
			var usedPorts = tcpConnInfoArray.Where(p => p.State != TcpState.Closed).Select(p => p.LocalEndPoint.Port).ToList();

			usedPorts.AddRange(usedServerTCpPorts.ToArray());
			usedPorts.AddRange(usedServerUdpPorts.ToArray());

			var unusedPort = 0;

			for (var port = portStartIndex; port < portEndIndex; port++)
			{
				if (usedPorts.Contains(port)) continue;
				unusedPort = port;
				break;
			}
			if (unusedPort == 0)
				throw new ApplicationException(message: "GetFreePortInRange, Out of ports");

			return unusedPort;
		}

		#endregion

		#region Constants

		/// <summary>	The minimum port. </summary>
		private const int MIN_PORT = 1000;

		/// <summary>	The maximum port. </summary>
		private const int MAX_PORT = 6000;

		#endregion
	}
}