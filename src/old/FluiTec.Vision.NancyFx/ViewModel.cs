using System.Collections.Generic;
using System.Linq;
using Nancy.Validation;

namespace FluiTec.Vision.NancyFx
{
	/// <summary>	A data Model for the view. </summary>
	public class ViewModel : IViewModel
	{
		#region Constructors

		/// <summary>	Default constructor. </summary>
		public ViewModel()
		{
			Errors = new Dictionary<string, IList<ModelValidationError>>();
		}

		#endregion

		#region Properties

		/// <summary>	Gets or sets the errors. </summary>
		/// <value>	The errors. </value>
		public IDictionary<string, IList<ModelValidationError>> Errors { get; set; }

		#endregion

		#region Methods

		/// <summary>	Gets the error messages. </summary>
		/// <value>	The error messages. </value>
		public IEnumerable<string> ErrorMessages => from error in Errors
			from modelError in error.Value
			select modelError.ErrorMessage;

		/// <summary>	Query if this object has errors. </summary>
		/// <returns>	True if errors, false if not. </returns>
		public bool HasErrors()
		{
			if (Errors == null) return false;
			return Errors.Count != 0;
		}

		/// <summary>	Enumerates error messages for in this collection. </summary>
		/// <param name="propertyName">	Name of the property. </param>
		/// <returns>
		///     An enumerator that allows foreach to be used to process error messages for in this collection.
		/// </returns>
		public IEnumerable<string> ErrorMessagesFor(string propertyName)
		{
			return from error in Errors
				from modelError in error.Value
				where error.Key == propertyName
				select modelError.ErrorMessage;
		}

		#endregion
	}
}