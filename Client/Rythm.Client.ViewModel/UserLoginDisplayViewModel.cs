// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using System.Windows.Input;

    using BusinessLogic;

    using Prism.Commands;
    using Prism.Mvvm;

    public class UserLoginDisplayViewModel : BindableBase
    {
        #region Fields

        private string _login;
        private readonly IUserLoginDisplayController _userLoginDisplayController;

        #endregion

        #region Properties

        public ICommand ConnectionSettingsCommand { get; }

        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        #endregion

        #region Constructors

        public UserLoginDisplayViewModel(IUserLoginDisplayController userLoginDisplayController)
        {
            _userLoginDisplayController = userLoginDisplayController;
            _userLoginDisplayController.NewUserLoginEvent += HandleNewLoginSelected;
            ConnectionSettingsCommand = new DelegateCommand(SettingsCommand);
        }

        #endregion

        #region Methods

        public void HandleNewLoginSelected(string login)
        {
            Login = login;
        }

        private void SettingsCommand()
        {
            //Here visibility changing
            _userLoginDisplayController.ConnectionParametersViewSettingsVisibility = false;
        }

        #endregion
    }
}
