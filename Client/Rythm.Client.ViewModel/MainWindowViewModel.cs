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

        #endregion

        #region Properties

        public GridLength SettingsVisibility
        {
            get => _viewVisibility;
            set => SetProperty(ref _viewVisibility, value);
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

        #endregion

        #region Constructors

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<ConnectionPanelVisibilityChangedEvent>().Subscribe(HandleConnectionParametersViewVisibility);
            eventAggregator.GetEvent<ConnectionIndicatorColorChangedEvent>().Subscribe(HandleConnectionIndicatorColorChanged);

            SettingsVisibility = new GridLength(10.0);
            SplitterVisibility = Visibility.Collapsed;
            _connectIndicatorColor = Brushes.LightPink;
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

        #endregion
    }
}
