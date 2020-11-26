// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using System.Windows.Input;

    using BusinessLogic;

    using Prism.Commands;

    public class UsersLoginsViewModel
    {
        #region Fields

        private readonly IUserListController _userListController;

        #endregion

        #region Properties

        public string Login { get; }
        public ICommand ChooseLoginCommand { get; }

        #endregion

        #region Constructors

        public UsersLoginsViewModel(IUserListController userListController, string login)
        {
            _userListController = userListController;
            ChooseLoginCommand = new DelegateCommand(ChosenLogin);
            Login = login;
        }

        #endregion

        #region Methods

        private void ChosenLogin()
        {
            _userListController.SendUserLogin(Login);
        }

        #endregion
    }
}
