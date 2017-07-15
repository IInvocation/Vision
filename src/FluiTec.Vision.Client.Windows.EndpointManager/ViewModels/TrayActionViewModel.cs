using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace FluiTec.Vision.Client.Windows.EndpointManager.ViewModels
{
	/// <summary>	A ViewModel for the tray action. </summary>
	public class TrayActionViewModel : ViewModelBase, ITrayItem
	{
		/// <summary>	The image source. </summary>
		private BitmapImage _imageSource;

		/// <summary>	The display text. </summary>
		private string _displayText;

		/// <summary>	The click command. </summary>
		private ICommand _clickCommand;

		/// <summary>	Constructor. </summary>
		/// <param name="text">			 	The text. </param>
		/// <param name="uriImageSource">	The URI image source. </param>
		/// <param name="action">		 	The action. </param>
		public TrayActionViewModel(string text, string uriImageSource, Action action)
		{
			DisplayText = text;
			ImageSource = new BitmapImage(new Uri(uriImageSource, UriKind.Relative));
			ClickCommand = new RelayCommand(action);
		}

		/// <summary>	Gets or sets the display text. </summary>
		/// <value>	The display text. </value>
		public string DisplayText
		{
			get => _displayText;
			set => Set(ref _displayText, value);
		}

		/// <summary>	Gets or sets the image source. </summary>
		/// <value>	The image source. </value>
		public BitmapImage ImageSource
		{
			get => _imageSource;
			set => Set(ref _imageSource, value);
		}

		/// <summary>	Gets or sets the click command. </summary>
		/// <value>	The click command. </value>
		public ICommand ClickCommand
		{
			get => _clickCommand;
			set => Set(ref _clickCommand, value);
		}
	}

	/// <summary>	A tray separator. </summary>
	public class TraySeparator : ITrayItem
	{
		
	}

	/// <summary>	Interface for tray item. </summary>
	public interface ITrayItem
	{
		
	}
}