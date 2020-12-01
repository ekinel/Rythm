// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using System;
    using System.Windows.Input;

    using BusinessLogic;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;

    public class UserLoginDisplayViewModel : BindableBase
    {
        #region Fields

        private string _login;
        private readonly IUserLoginDisplayController _userLoginDisplayController;
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

        public UserLoginDisplayViewModel(IUserLoginDisplayController userLoginDisplayController, IEventAggregator eventAggregator)
        {
            _userLoginDisplayController = userLoginDisplayController ?? throw new ArgumentNullException(nameof(userLoginDisplayController));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            _userLoginDisplayController.NewUserLoginEvent += HandleNewLoginSelected;
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
