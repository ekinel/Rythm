// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using BusinessLogic;

    using Prism.Mvvm;

    public class UserLoginDisplayViewModel : BindableBase
    {
        #region Fields

        private string _login;
        private readonly IUserLoginDisplayController _userLoginDisplayController;

        #endregion

        #region Properties

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
        }

        #endregion

        #region Methods

        public void HandleNewLoginSelected(string login)
        {
            Login = login;
        }

        #endregion
    }
}
