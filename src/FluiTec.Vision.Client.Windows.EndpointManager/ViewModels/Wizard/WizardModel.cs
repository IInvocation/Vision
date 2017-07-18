using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using Xceed.Wpf.Toolkit;

namespace FluiTec.Vision.Client.Windows.EndpointManager.ViewModels.Wizard
{
	/// <summary>	A data Model for the wizard. </summary>
	public class WizardModel : ViewModelBase
	{
		/// <summary>	Dictionary of wizards. </summary>
		private readonly Dictionary<WizardPage, WizardPageViewModel> _wizardDictionary =
			new Dictionary<WizardPage, WizardPageViewModel>();

		/// <summary>	The current page. </summary>
		private WizardPageViewModel _currentPage;

		/// <summary>	The current x coordinate page. </summary>
		private WizardPage _currentXPage;

		private IReadOnlyList<WizardPageViewModel> _pages;

		/// <summary>	Gets or sets the pages. </summary>
		/// <value>	The pages. </value>
		public IReadOnlyList<WizardPageViewModel> Pages
		{
			get => _pages;
			set
			{
				_pages = value;
				WireUpPages();
			}
		}

		/// <summary>	Wire up pages. </summary>
		private void WireUpPages()
		{
			if (Pages == null || Pages.Count < 1) return;
			for (var i = 0; i < Pages.Count; i++)
			{
				if (i > 0)
					Pages[i].Previous = Pages[i - 1];
				if (i < Pages.Count - 1)
					Pages[i].Next = Pages[i + 1];
			}
		}

		/// <summary>	Gets the pages. </summary>
		/// <value>	The x coordinate pages. </value>
		public List<WizardPage> XPages
		{
			get
			{
				// make sure not to project twice
				if (_wizardDictionary != null && _wizardDictionary.Count >= 1)
					return _wizardDictionary.Select(kv => kv.Key).ToList();

				// project
				var xpages = Pages
					.Select(Project)
					.ToList();
				if (CurrentPage == null)
					CurrentPage = Pages.First();
				return xpages;
			}
		}

		/// <summary>	True if we can select next page, false if not. </summary>
		public bool CanSelectNextPage => CurrentPage?.NextEnabled ?? false;

		/// <summary>	True if this object can finish. </summary>
		public bool CanFinish => CurrentPage?.FinishEnabled ?? false;

		/// <summary>	Gets or sets the current x coordinate page. </summary>
		/// <value>	The current x coordinate page. </value>
		public WizardPage CurrentXPage
		{
			get => _currentXPage;
			set
			{
				_currentXPage = value;
				if (!CurrentPage.Equals(_wizardDictionary[value]))
					CurrentPage = _wizardDictionary[value];
			}
		}

		/// <summary>	Gets or sets the current page. </summary>
		/// <value>	The current page. </value>
		public WizardPageViewModel CurrentPage
		{
			get => _currentPage;
			set
			{
				if (_currentPage != null)
					_currentPage.PropertyChanged -= _currentPage_PropertyChanged;
				_currentPage = value;
				
				_currentPage.PropertyChanged += _currentPage_PropertyChanged;

				_currentPage.Validate();

				var xpage = _wizardDictionary.Single(p => p.Value == value).Key;
				if (!xpage.Equals(CurrentXPage))
					CurrentXPage = xpage;
			}
		}

		/// <summary>	Event handler. Called by _currentPage for property changed events. </summary>
		/// <param name="sender">	Source of the event. </param>
		/// <param name="e">	 	Property changed event information. </param>
		private void _currentPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(WizardPageViewModel.NextEnabled))
				RaisePropertyChanged(nameof(CanSelectNextPage));
			if (e.PropertyName == nameof(WizardPageViewModel.FinishEnabled))
				RaisePropertyChanged(nameof(CanFinish));
		}

		/// <summary>	Projects the given model. </summary>
		/// <param name="model">	The model. </param>
		/// <returns>	A WizardPage. </returns>
		private WizardPage Project(WizardPageViewModel model)
		{
			var page = new WizardPage
			{
				Title = model.Title,
				Description = model.Description,
				Content = model.Content,
				DataContext = model,
				PageType = WizardPageType.Blank,
				Background = new SolidColorBrush(Colors.White),
				Foreground = new SolidColorBrush(Colors.Black)
			};

			_wizardDictionary.Add(page, model);

			return page;
		}
	}
}