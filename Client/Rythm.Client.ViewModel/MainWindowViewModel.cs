// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
	using System.Windows;
	using System.Windows.Media;

	using Events;

	using Prism.Events;
	using Prism.Mvvm;

	public class MainWindowViewModel : BindableBase
	{
		#region Fields

		private GridLength _viewVisibility;
		private bool _toggleSettingsVisibility;
		private Visibility _splitterVisibility;
		private Brush _connectIndicatorColor;

		private GridLength _viewUsersListVisibility;

		private bool _toggleDisplayingEventsVisibility;
		private Visibility _splitterSplitterDisplayingEventsVisibility;

		private Visibility _viewDisplayingEventsVisibility;
		private bool _stackPanelVisibility;

		private Visibility _viewChatPanelVisibility;

		#endregion

		#region Properties

		public GridLength SettingsVisibility
		{
			get => _viewVisibility;
			set => SetProperty(ref _viewVisibility, value);
		}

		public GridLength UsersListVisibility
		{
			get => _viewUsersListVisibility;
			set => SetProperty(ref _viewUsersListVisibility, value);
		}

		public Visibility SplitterVisibility
		{
			get => _splitterVisibility;
			set => SetProperty(ref _splitterVisibility, value);
		}

		public Brush ConnectIndicatorColor
		{
			get => _connectIndicatorColor;
			set => SetProperty(ref _connectIndicatorColor, value);
		}

		public Visibility SplitterDisplayingEventsVisibility
		{
			get => _splitterSplitterDisplayingEventsVisibility;
			set => SetProperty(ref _splitterSplitterDisplayingEventsVisibility, value);
		}

		public Visibility DisplayingEventsVisibility
		{
			get => _viewDisplayingEventsVisibility;
			set => SetProperty(ref _viewDisplayingEventsVisibility, value);
		}

		public Visibility ChatPanelVisibility
		{
			get => _viewChatPanelVisibility;
			set => SetProperty(ref _viewChatPanelVisibility, value);
		}

		#endregion

		#region Constructors

		public MainWindowViewModel(IEventAggregator eventAggregator)
		{
			eventAggregator.GetEvent<ConnectionPanelVisibilityChangedEvent>().Subscribe(HandleConnectionParametersViewVisibility);
			eventAggregator.GetEvent<ConnectionIndicatorColorChangedEvent>().Subscribe(HandleConnectionIndicatorColorChanged);
			eventAggregator.GetEvent<DisplayingEventsVisibility>().Subscribe(HandleDisplayingEventsViewVisibility);

			SettingsVisibility = new GridLength(10.0);
			SplitterVisibility = Visibility.Collapsed;
			_connectIndicatorColor = Brushes.LightPink;

			SplitterDisplayingEventsVisibility = Visibility.Visible;
			UsersListVisibility = new GridLength(0.15, GridUnitType.Star);

			DisplayingEventsVisibility = Visibility.Collapsed;
			ChatPanelVisibility = Visibility.Visible;
		}

		#endregion

		#region Methods

		private void HandleConnectionIndicatorColorChanged(bool isConnected)
		{
			ConnectIndicatorColor = isConnected ? Brushes.LightGreen : Brushes.LightPink;
		}

		private void HandleConnectionParametersViewVisibility()
		{
			_toggleSettingsVisibility = !_toggleSettingsVisibility;
			SettingsVisibility = _toggleSettingsVisibility ? new GridLength(0.2, GridUnitType.Star) : new GridLength(10.0);
			SplitterVisibility = _toggleSettingsVisibility ? Visibility.Visible : Visibility.Collapsed;
		}

		private void HandleDisplayingEventsViewVisibility()
		{
			_toggleDisplayingEventsVisibility = !_toggleDisplayingEventsVisibility;
			SplitterDisplayingEventsVisibility = _toggleDisplayingEventsVisibility ? Visibility.Collapsed : Visibility.Visible;
			UsersListVisibility = _toggleDisplayingEventsVisibility ? new GridLength(0.0) : new GridLength(0.15, GridUnitType.Star);

			_stackPanelVisibility = !_stackPanelVisibility;
			ChatPanelVisibility = _stackPanelVisibility ? Visibility.Collapsed : Visibility.Visible;
			DisplayingEventsVisibility = _stackPanelVisibility ? Visibility.Visible : Visibility.Collapsed;
		}

		#endregion
	}
}
