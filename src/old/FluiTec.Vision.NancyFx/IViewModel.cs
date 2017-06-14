using System.Collections.Generic;
using Nancy.Validation;

namespace FluiTec.Vision.NancyFx
{
	/// <summary>	Interface for view model. </summary>
	public interface IViewModel
	{
		#region Properties

		/// <summary>	Gets or sets the errors. </summary>
		/// <value>	The errors. </value>
		IDictionary<string, IList<ModelValidationError>> Errors { get; set; }

		/// <summary>	Gets the error messages. </summary>
		/// <value>	The error messages. </value>
		IEnumerable<string> ErrorMessages { get; }

		#endregion

		#region Methods

		/// <summary>	Query if this object has errors. </summary>
		/// <returns>	True if errors, false if not. </returns>
		bool HasErrors();

		/// <summary>	Enumerates error messages for in this collection. </summary>
		/// <param name="propertyName">	Name of the property. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process error messages for in this collection.
		/// </returns>
		IEnumerable<string> ErrorMessagesFor(string propertyName);

		#endregion
	}
}