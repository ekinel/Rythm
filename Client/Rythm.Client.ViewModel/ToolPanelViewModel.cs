// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using System;
    using System.Windows.Input;

    using Events;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;

    public class ToolPanelViewModel : BindableBase
    {
        #region Fields

        private string _login;
        private readonly IEventAggregator _eventAggregator;

        #endregion

        #region Properties

        public ICommand ChangeSettingsVisibilityCommand { get; }

        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        #endregion

        #region Constructors

        public ToolPanelViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _eventAggregator.GetEvent<NewClientChosenViewModel>().Subscribe(HandleNewLoginSelected);
            ChangeSettingsVisibilityCommand = new DelegateCommand(ExecuteChangeSettingsVisibilityCommand);
        }

        #endregion

        #region Methods

        public void HandleNewLoginSelected(string login)
        {
            Login = login;
        }

        private void ExecuteChangeSettingsVisibilityCommand()
        {
            _eventAggregator.GetEvent<ConnectionPanelVisibilityChangedEvent>().Publish();
        }

        #endregion
    }
}
