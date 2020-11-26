// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using System.Collections.ObjectModel;

    using BusinessLogic;

    using Prism.Mvvm;

    public class UserListViewModel : BindableBase
    {
        #region Fields

        private static IUserListController _userListController;

        #endregion

        #region Properties

        public ObservableCollection<UsersLoginsViewModel> UserList { get; set; }

        #endregion

        #region Constructors

        public UserListViewModel(IUserListController userListController)
        {
            _userListController = userListController;
            UserList = new ObservableCollection<UsersLoginsViewModel>
            {
                new UsersLoginsViewModel(_userListController, "Никита"),
                new UsersLoginsViewModel(_userListController, "Катя"),
                new UsersLoginsViewModel(_userListController, "Паша")
            };
        }

        #endregion
    }
}
