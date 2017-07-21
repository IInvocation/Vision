using System;

namespace FluiTec.AppFx.Upnp
{
	/// <summary>	Exception for signalling upnp errors. </summary>
	public class UpnpException : Exception
	{
		/// <summary>	Constructor. </summary>
		/// <param name="message">		 	The message. </param>
		/// <param name="innerException">	The inner exception. </param>
		public UpnpException(string message, Exception innerException) : base(message, innerException) { }
	}
}