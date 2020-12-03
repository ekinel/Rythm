// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Threading;

    using BusinessLogic.Interfaces;

    using Prism.Events;
    using Prism.Mvvm;

    public class UserListViewModel : BindableBase
    {
        #region Fields

        private readonly IUserListController _userListController;
        private readonly IEventAggregator _eventAggregator;

        #endregion

        #region Properties

        public ObservableCollection<UsersLoginsViewModel> UserList { get; set; }

        #endregion

        #region Constructors

        public UserListViewModel(IEventAggregator eventAggregator, IUserListController userListController)
        {
            _eventAggregator = eventAggregator;
            _userListController = userListController;
            _userListController.UpdatedUsersListEvent += HandleUpdatedUsersList;

            UserList = new ObservableCollection<UsersLoginsViewModel>();

            //UserList = new ObservableCollection<UsersLoginsViewModel>
            //{
            //    new UsersLoginsViewModel(eventAggregator, "Никита"),
            //    new UsersLoginsViewModel(eventAggregator, "Катя"),
            //    new UsersLoginsViewModel(eventAggregator, "Паша")
            //};
        }

        #endregion

        #region Methods

        public void HandleUpdatedUsersList(List<string> updatedUsersList)
        {
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(
                    () =>
                    {
                        UserList.Clear();
                    }));

            foreach (string user in updatedUsersList)
            {
                Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Background,
                    new Action(
                        () =>
                        {
                            UserList.Add(new UsersLoginsViewModel(_eventAggregator, user));
                        }));
            }
        }

        #endregion
    }
}
