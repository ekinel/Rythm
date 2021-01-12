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
		private bool _splitterSplitterDisplayingEventsVisibility;

		private bool _chatPanelVisibility;
		private bool _eventsPanelVisibility;
		#endregion

		#region Properties

		public bool ChatPanelVisibility
		{
			get => _chatPanelVisibility;
			set => SetProperty(ref _chatPanelVisibility, value);
		}

		public bool EventPanelVisibility
		{
			get => _eventsPanelVisibility;
			set => SetProperty(ref _eventsPanelVisibility, value);
		}

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

		public bool SplitterDisplayingEventsVisibility
		{
			get => _splitterSplitterDisplayingEventsVisibility;
			set => SetProperty(ref _splitterSplitterDisplayingEventsVisibility, value);
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

			SplitterDisplayingEventsVisibility = true;
			UsersListVisibility = new GridLength(0.15, GridUnitType.Star);

			ChatPanelVisibility = true;
			EventPanelVisibility = false;
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
			UsersListVisibility = _toggleDisplayingEventsVisibility ? new GridLength(0.0) : new GridLength(0.15, GridUnitType.Star);

			ChatPanelVisibility = !ChatPanelVisibility;
			EventPanelVisibility = !EventPanelVisibility;
		
		}

		#endregion
	}
}
