using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using FluiTec.Vision.Client.Windows.EndpointHelper.Helpers;
using FluiTech.AppFx.Ssl;
using Org.BouncyCastle.Crypto;

namespace FluiTec.Vision.Client.Windows.EndpointHelper.Configuration
{
	/// <summary>	A HTTP configuration. </summary>
	public class HttpConfiguration
	{
		#region Properties

		/// <summary>	Gets or sets the name of the application. </summary>
		/// <value>	The name of the application. </value>
		public string ApplicationName { get; set; }

		/// <summary>	Gets or sets a value indicating whether the remove firewall exception. </summary>
		/// <value>	True if remove firewall exception, false if not. </value>
		public bool RemoveFirewallException { get; set; }

		/// <summary>	Gets or sets the remove firewall exception port. </summary>
		/// <value>	The remove firewall exception port. </value>
		public int? RemoveFirewallExceptionPort { get; set; }

		/// <summary>	Gets or sets a value indicating whether the add firewall exception. </summary>
		/// <value>	True if add firewall exception, false if not. </value>
		public bool AddFirewallException { get; set; }

		/// <summary>	Gets or sets the add firewall exception port. </summary>
		/// <value>	The add firewall exception port. </value>
		public int? AddFirewallExceptionPort { get; set; }

		/// <summary>	Gets or sets a value indicating whether the remove ssl certificate. </summary>
		/// <value>	True if remove ssl certificate, false if not. </value>
		public bool RemoveSslCertificate { get; set; }

		/// <summary>	Gets or sets the remove ssl certificate port. </summary>
		/// <value>	The remove ssl certificate port. </value>
		public int? RemoveSslCertificatePort { get; set; }

		/// <summary>	Gets or sets a value indicating whether the add ssl certificate. </summary>
		/// <value>	True if add ssl certificate, false if not. </value>
		public bool AddSslCertificate { get; set; }

		/// <summary>	Gets or sets the add ssl certificate port. </summary>
		/// <value>	The add ssl certificate port. </value>
		public int? AddSslCertificatePort { get; set; }

		/// <summary>	Gets or sets the identifier of the add ssl certificate application. </summary>
		/// <value>	The identifier of the add ssl certificate application. </value>
		public Guid? AddSslCertificateApplicationId { get; set; }

		/// <summary>	Gets or sets a value indicating whether the remove URL reservation. </summary>
		/// <value>	True if remove URL reservation, false if not. </value>
		public bool RemoveUrlReservation { get; set; }

		/// <summary>	Gets or sets URI of the remove URL reservation. </summary>
		/// <value>	The remove URL reservation URI. </value>
		public string RemoveUrlReservationUri { get; set; }

		/// <summary>	Gets or sets a value indicating whether the add URL reservation. </summary>
		/// <value>	True if add URL reservation, false if not. </value>
		public bool AddUrlReservation { get; set; }

		/// <summary>	Gets or sets URI of the add URL reservation. </summary>
		/// <value>	The add URL reservation URI. </value>
		public string AddUrlReservationUri { get; set; }

		/// <summary>	Gets a value indicating whether this object is valid. </summary>
		/// <value>	True if this object is valid, false if not. </value>
		public bool IsValid
		{
			get
			{
				return new[]
				{
					string.IsNullOrWhiteSpace(ApplicationName),
					RemoveFirewallException && !RemoveFirewallExceptionPort.HasValue,
					AddFirewallException && !AddFirewallExceptionPort.HasValue,
					RemoveSslCertificate && !RemoveSslCertificatePort.HasValue,
					AddSslCertificate && !AddSslCertificatePort.HasValue && !AddSslCertificateApplicationId.HasValue,
					RemoveUrlReservation && string.IsNullOrWhiteSpace(RemoveUrlReservationUri),
					AddUrlReservation && string.IsNullOrWhiteSpace(AddUrlReservationUri)
				}.Any(b => !b);
			}
		}

		#endregion

		#region Methods
		
		/// <summary>	Runs this object. </summary>
		public void Run()
		{
			ExecuteRemoveFirewallException();
			ExecuteAddFirewallException();
			ExecuteRemoveSslCertificate();
			ExecuteAddSslCertificate();
			ExecuteRemoveUrlReservation();
			ExecuteAddUrlReservation();
		}

		/// <summary>	Executes the remove firewall exception operation. </summary>
		private void ExecuteRemoveFirewallException()
		{
			if (!RemoveFirewallException) return;
			
			ConsoleHelper.ReportStatus($"Removing firewall-exception for port {RemoveFirewallExceptionPort}");

			var cmdArgs = $"advfirewall firewall delete rule name=\"{ApplicationName}{RemoveFirewallExceptionPort}\" " +
			              $"protocol=TCP localport={RemoveFirewallExceptionPort}";

			new Process
				{
					StartInfo = new ProcessStartInfo(fileName: "netsh")
					{
						Arguments = cmdArgs,
						UseShellExecute = false,
						Verb = "runas"
					}
				}
				.RedirectOutputToConsole()
				.RunAndWait();


		}

		/// <summary>	Executes the add firewall exception operation. </summary>
		private void ExecuteAddFirewallException()
		{
			if (!AddFirewallException) return;

			ConsoleHelper.ReportStatus($"Adding firewall-exception for port {AddFirewallExceptionPort}");

			var cmdArgs =
				$"advfirewall firewall add rule name=\"{ApplicationName}{AddFirewallExceptionPort}\" dir=in action=allow protocol=TCP localport={AddFirewallExceptionPort}";

			var ok = new Process
				{
					StartInfo = new ProcessStartInfo(fileName: "netsh")
					{
						Arguments = cmdArgs,
						UseShellExecute = false,
						Verb = "runas"
					}
				}
				.RedirectOutputToConsole()
				.RunAndWait();
			if (!ok)
			{
				throw new Exception($"Could not add firewall-exception for port {AddFirewallExceptionPort}");
			}

			// save changed settings for auto-delete
			RemoveFirewallException = true;
			RemoveFirewallExceptionPort = AddFirewallExceptionPort;
		}

		/// <summary>	Executes the remove ssl certificate operation. </summary>
		private void ExecuteRemoveSslCertificate()
		{
			if (!RemoveSslCertificate) return;

			ConsoleHelper.ReportStatus($"Removing ssl-certificate for port {RemoveSslCertificatePort}");

			var cn = $"CN={Environment.MachineName}-{ApplicationName}";
			var caCn = $"CN={Environment.MachineName}-{ApplicationName}-CA";

			try
			{
				SslHelper.RemoveCertsFromStore(cn, StoreName.My, StoreLocation.LocalMachine);
				SslHelper.RemoveCertsFromStore(caCn, StoreName.CertificateAuthority, StoreLocation.LocalMachine);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				throw;
			}
			
			var cmdArgs = $"http delete sslcert ipport=0.0.0.0:{RemoveSslCertificatePort}";

			new Process
				{
					StartInfo = new ProcessStartInfo(fileName: "netsh")
					{
						Arguments = cmdArgs,
						UseShellExecute = false,
						Verb = "runas"
					}
				}
				.RedirectOutputToConsole()
				.RunAndWait();
		}

		/// <summary>	Executes the add ssl certificate operation. </summary>
		private void ExecuteAddSslCertificate()
		{
			var certHash = "";
			if (!AddSslCertificate) return;

			ConsoleHelper.ReportStatus($"Adding ssl-certificate for port {AddSslCertificatePort}");

			RetryHelper.Do(() =>
			{
				// create ca
				var caCert = SslHelper.GenerateCaCertificate($"CN={Environment.MachineName}-{ApplicationName}-CA", out AsymmetricKeyParameter caPrivateKey, DateTime.Now.AddYears(value: 10));

				// add ca-cert to store
				SslHelper.AddCertToStore(caCert, StoreName.My, StoreLocation.LocalMachine);

				// create selfsigned cert
				var cert = SslHelper.GenerateSelfSignedCertificate($"CN={Environment.MachineName}-{ApplicationName}", $"CN={Environment.MachineName}-{ApplicationName}-CA", caPrivateKey, DateTime.Now.AddYears(value: 10));

				// add cert to store
				var p12 = cert.Export(X509ContentType.Pfx);
				SslHelper.AddCertToStore(new X509Certificate2(p12, (string)null, X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet), StoreName.My, StoreLocation.LocalMachine);

				// get thumbprint (used by netsh, etc.)
				certHash = cert.Thumbprint?.ToLower();
			}, TimeSpan.Zero);

			var cmdArgs = $"http add sslcert ipport=0.0.0.0:{AddSslCertificatePort} certhash={certHash} appid={{{AddSslCertificateApplicationId}}}";

			var ok = new Process
				{
					StartInfo = new ProcessStartInfo(fileName: "netsh")
					{
						Arguments = cmdArgs,
						UseShellExecute = false,
						Verb = "runas"
					}
				}
				.RedirectOutputToConsole()
				.RunAndWait();

			if (!ok)
			{
				throw new Exception($"Could not add ssl-certificate for port {AddSslCertificatePort}");
			}

			// save changed settings for auto-delete
			RemoveSslCertificate = true;
			RemoveSslCertificatePort = AddSslCertificatePort;
		}

		/// <summary>	Executes the remove URL reservation operation. </summary>
		private void ExecuteRemoveUrlReservation()
		{
			if (!RemoveUrlReservation) return;

			ConsoleHelper.ReportStatus($"Removing url-reservation for uri {RemoveUrlReservationUri}");

			var cmdArgs = $"http delete urlacl url={RemoveUrlReservationUri}";

			new Process
				{
					StartInfo = new ProcessStartInfo(fileName: "netsh")
					{
						Arguments = cmdArgs,
						UseShellExecute = false,
						Verb = "runas"
					}
				}
				.RedirectOutputToConsole()
				.RunAndWait();
		}

		/// <summary>	Executes the add URL reservation operation. </summary>
		private void ExecuteAddUrlReservation()
		{
			if (!AddUrlReservation) return;

			ConsoleHelper.ReportStatus($"Adding url-reservation for uri {AddUrlReservationUri}");

			var sid = new SecurityIdentifier(WellKnownSidType.WorldSid, domainSid: null);
			var account = (NTAccount)sid.Translate(typeof(NTAccount));

			var cmdArgs = $"http add urlacl url={AddUrlReservationUri} user={account.Value}";

			var ok = new Process
				{
					StartInfo = new ProcessStartInfo(fileName: "netsh")
					{
						Arguments = cmdArgs,
						UseShellExecute = false,
						Verb = "runas"
					}
				}
				.RedirectOutputToConsole()
				.RunAndWait();

			if (!ok)
			{
				throw new Exception($"Could not add url-reservation for url {AddUrlReservationUri}");
			}

			// save changed settings for auto-delete
			RemoveUrlReservation = true;
			RemoveUrlReservationUri = AddUrlReservationUri;
		}

		#endregion
	}
}