using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;

namespace FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.SetupWizard.Actions
{
	/// <summary>	A validation action. </summary>
	public class ValidationAction : ViewModelBase, IValidationAction
	{
		#region Fields

		/// <summary>	Name of the display. </summary>
		private string _displayName;

		/// <summary>	The status. </summary>
		private ActionStatus _status;

		/// <summary>	The status image. </summary>
		private BitmapImage _statusImage;

		/// <summary>	The action to execute. </summary>
		private Func<Task<ValidationResult>> _actionToExecute;

		/// <summary>	Message describing the error. </summary>
		private string _errorMessage;

		#endregion

		#region Constructors

		/// <summary>	Default constructor. </summary>
		public ValidationAction()
		{
			Status = ActionStatus.Waiting;
		}

		#endregion

		#region Properties

		/// <summary>	Gets or sets the name of the display. </summary>
		/// <value>	The name of the display. </value>
		public string DisplayName
		{
			get => _displayName;
			set => Set(ref _displayName, value);
		}

		/// <summary>	Gets or sets the status. </summary>
		/// <value>	The status. </value>
		public ActionStatus Status
		{
			get => _status;
			set
			{
				Set(ref _status, value);
				StatusImage = FromStatus(_status);
				RaisePropertyChanged(nameof(IsFailed));
			}
		}

		/// <summary>	Gets or sets the status image. </summary>
		/// <value>	The status image. </value>
		public BitmapImage StatusImage
		{
			get => _statusImage;
			set => Set(ref _statusImage, value);
		}

		/// <summary>	Gets the action to execute. </summary>
		/// <value>	The action to execute. </value>
		public Func<Task<ValidationResult>> ActionToExecute
		{
			get => _actionToExecute;
			set => Set(ref _actionToExecute, value);
		}

		/// <summary>	Gets or sets a message describing the error. </summary>
		/// <value>	A message describing the error. </value>
		public string ErrorMessage
		{
			get => _errorMessage;
			set => Set(ref _errorMessage, value);
		}

		/// <summary>	The status. </summary>
		public bool IsFailed => Status == ActionStatus.Failed;

		#endregion

		#region Methods

		/// <summary>	Initializes this object from the given from status. </summary>
		/// <param name="status">	The status. </param>
		/// <returns>	A BitmapImage. </returns>
		private BitmapImage FromStatus(ActionStatus status)
		{
			string statusImageName;
			switch (status)
			{
				case ActionStatus.Waiting:
					statusImageName = "waiting";
					break;
				case ActionStatus.Executing:
					statusImageName = "executing";
					break;
				case ActionStatus.Successed:
					statusImageName = "successed";
					break;
				case ActionStatus.Failed:
					statusImageName = "failed";
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(status), status, null);
			}

			return new BitmapImage(new Uri(@"pack://application:,,,/"
			                       + Assembly.GetExecutingAssembly().GetName().Name
			                       + ";component/"
			                       + $"Resources/Images/status_{statusImageName}.png", UriKind.Absolute));
		}

		/// <summary>	Runs the given settings. </summary>
		public async Task<ValidationResult> Run()
		{
			Status = ActionStatus.Executing;
			try
			{
				var result = await ActionToExecute.Invoke();
				Status = result.Success ? ActionStatus.Successed : ActionStatus.Failed;
				ErrorMessage = result.ErrorMessage;
				return result;
			}
			catch (Exception e)
			{
				Status = ActionStatus.Failed;
				ErrorMessage = e.Message;
				return new ValidationResult
				{
					Success = false,
					ErrorMessage = ErrorMessage
				};
			}
		}

		#endregion
	}
}