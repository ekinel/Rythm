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

    public class UsersListViewModel : BindableBase
    {
        #region Fields

        private readonly IUsersListController _usersListController;
        private readonly IEventAggregator _eventAggregator;

        #endregion

        #region Properties

        public ObservableCollection<UsersLoginsViewModel> UserList { get; set; }

        #endregion

        #region Constructors

        public UsersListViewModel(IEventAggregator eventAggregator, IUsersListController usersListController)
        {
            _eventAggregator = eventAggregator;
            _usersListController = usersListController;
            _usersListController.UpdatedUsersListEvent += HandleUpdatedUsersList;

            UserList = new ObservableCollection<UsersLoginsViewModel>();
        }

        #endregion

        #region Methods

        public void HandleUpdatedUsersList(List<string> activeUsersList, List<string> notActiveUsersList)
        {
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(
                    () =>
                    {
                        UserList.Clear();
                    }));

            foreach (string user in activeUsersList)
            {
                Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Background,
                    new Action(
                        () =>
                        {
                            UserList.Add(new UsersLoginsViewModel(_eventAggregator, user, true));
                        }));
            }

            foreach (string user in notActiveUsersList)
            {
	            Application.Current.Dispatcher.BeginInvoke(
		            DispatcherPriority.Background,
		            new Action(
			            () =>
			            {
				            UserList.Add(new UsersLoginsViewModel(_eventAggregator, user, false));
			            }));
            }
        }

        #endregion
    }
}
