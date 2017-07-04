using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Fluitec.Vision.Client.WindowsClient.Converter
{
	/// <summary>	to image. </summary>
	public class BoolToImageConverter : IValueConverter
	{
		/// <summary>	Gets or sets the true image. </summary>
		/// <value>	The true image. </value>
		public BitmapImage TrueImage { get; set; }

		/// <summary>	Gets or sets the false image. </summary>
		/// <value>	The false image. </value>
		public BitmapImage FalseImage { get; set; }

		/// <summary>	Converts. </summary>
		/// <param name="value">	 	The value. </param>
		/// <param name="targetType">	Type of the target. </param>
		/// <param name="parameter"> 	The parameter. </param>
		/// <param name="culture">   	The culture. </param>
		/// <returns>	An object. </returns>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is bool))
				return null;

			var b = (bool) value;
			return b ? TrueImage : FalseImage;
		}

		/// <summary>	Convert back. </summary>
		/// <param name="value">	 	The value. </param>
		/// <param name="targetType">	Type of the target. </param>
		/// <param name="parameter"> 	The parameter. </param>
		/// <param name="culture">   	The culture. </param>
		/// <returns>	The back converted. </returns>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}