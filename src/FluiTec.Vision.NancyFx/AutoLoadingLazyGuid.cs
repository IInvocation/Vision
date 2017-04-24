using System;

// ReSharper disable once CheckNamespace
namespace Nancy
{
	/// <summary>	An automatic loading lazy unique identifier. </summary>
	public class AutoLoadingLazyGuid : Lazy<Guid>
	{
		/// <summary>	Constructor. </summary>
		/// <param name="func">	The function. </param>
		public AutoLoadingLazyGuid(Func<Guid> func) : base(func)
		{
		}

		/// <summary>	Convert this object into a string representation. </summary>
		/// <returns>	A string that represents this object. </returns>
		public override string ToString()
		{
			return Value.ToString();
		}
	}
}