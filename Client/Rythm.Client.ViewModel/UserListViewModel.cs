// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    using BusinessLogic;

    using Prism.Commands;
    using Prism.Mvvm;

    public class UserListViewModel : BindableBase
    {
        #region Fields

        private static IUserListController _userListController;

        #endregion

        #region Properties

        public ObservableCollection<UsersLogins> UserList { get; set; } = new ObservableCollection<UsersLogins>
        {
            new UsersLogins(_userListController, "Никита"),
            new UsersLogins(_userListController, "Катя"),
            new UsersLogins(_userListController, "Паша")
        };

        #endregion

        #region Constructors

        public UserListViewModel(IUserListController userListController)
        {
            _userListController = userListController;
        }

        #endregion
    }

    public class UsersLogins
    {
        #region Fields

        private readonly IUserListController _userListController;

        #endregion

        #region Properties

        public string Login { get; }
        public ICommand ChooseLoginCommand { get; }

        #endregion

        #region Constructors

        public UsersLogins(IUserListController userListController, string login)
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
