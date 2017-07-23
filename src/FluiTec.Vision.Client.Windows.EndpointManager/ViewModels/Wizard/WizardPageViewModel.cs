﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.Wizard
{
	public class WizardPageViewModel : INotifyPropertyChanged
	{
		#region Methods

		/// <summary>	Executes the property changed action. </summary>
		/// <param name="propertyName">	(Optional) Name of the property. </param>
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>	Validates this object. </summary>
		public void Validate()
		{
			IsValid = ValidateModel();
		}

		/// <summary>	Validates the model. </summary>
		/// <returns>	True if it succeeds, false if it fails. </returns>
		protected virtual bool ValidateModel()
		{
			return false;
		}

		#endregion

		#region Fields

		/// <summary>	Event queue for all listeners interested in PropertyChanged events. </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		private WizardPageViewModel _previous;
		private WizardPageViewModel _next;
		private bool _isValid;
		private ContentControl _content;

		#endregion

		#region Properties

		/// <summary>	Gets or sets the title. </summary>
		/// <value>	The title. </value>
		public string Title { get; set; }

		/// <summary>	Gets or sets the description. </summary>
		/// <value>	The description. </value>
		public string Description { get; set; }

		/// <summary>	Gets or sets the content. </summary>
		/// <value>	The content. </value>
		public ContentControl Content
		{
			get => _content;
			set
			{
				_content = value;
				if (_content != null)
					_content.DataContext = this;
				OnPropertyChanged();
			}
		}

		/// <summary>	Gets or sets the previous. </summary>
		/// <value>	The previous. </value>
		public WizardPageViewModel Previous
		{
			get => _previous;
			set
			{
				_previous = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(BackVisible));
				OnPropertyChanged(nameof(BackEnabled));
			}
		}

		/// <summary>	Gets or sets the next. </summary>
		/// <value>	The next. </value>
		public WizardPageViewModel Next
		{
			get => _next;
			set
			{
				_next = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(NextVisible));
				OnPropertyChanged(nameof(FinishVisible));
			}
		}

		/// <summary>	Gets or sets a value indicating whether this object is valid. </summary>
		/// <value>	True if this object is valid, false if not. </value>
		public bool IsValid
		{
			get => _isValid;
			set
			{
				_isValid = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(NextEnabled));
				OnPropertyChanged(nameof(FinishEnabled));
			}
		}

		/// <summary>	True to make back visible, false for invisible. </summary>
		public virtual bool BackVisible => Previous != null;

		/// <summary>	True to make back enabled, false for disabled. </summary>
		public virtual bool BackEnabled => Previous != null;

		/// <summary>	True to make next visible, false for invisible. </summary>
		public virtual bool NextVisible => Next != null;

		/// <summary>	True to enable, false to disable the next. </summary>
		public virtual bool NextEnabled => IsValid;

		/// <summary>	True to make finish visible, false for invisible. </summary>
		public virtual bool FinishVisible => Next == null;

		/// <summary>	True to enable, false to disable the finish. </summary>
		public virtual bool FinishEnabled => IsValid && Next == null;

		#endregion
	}
}