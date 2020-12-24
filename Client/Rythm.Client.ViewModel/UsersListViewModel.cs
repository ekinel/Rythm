// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;

	using BusinessLogic.Interfaces;

	using Prism.Events;
	using Prism.Mvvm;

	public class UsersListViewModel : BindableBase
	{
		#region Fields

		private readonly IEventAggregator _eventAggregator;

		#endregion

		#region Properties

		public ObservableCollection<UsersLoginsViewModel> UserList { get; set; }

		#endregion

		#region Constructors

		public UsersListViewModel(IEventAggregator eventAggregator, IUsersListController usersListController)
		{
			_eventAggregator = eventAggregator;
			usersListController.UpdatedUsersListEvent += HandleUpdatedUsersList;

			UserList = new ObservableCollection<UsersLoginsViewModel>();
		}

		#endregion

		#region Methods

		public void HandleUpdatedUsersList(List<string> activeUsersList, List<string> notActiveUsersList)
		{
			UserList.Clear();
			UserList.Add(new UsersLoginsViewModel(_eventAggregator, Properties.Resources.CommonChat, true));
			foreach (string user in activeUsersList)
			{
				UserList.Add(new UsersLoginsViewModel(_eventAggregator, user, true));
			}

			foreach (string user in notActiveUsersList)
			{
				UserList.Add(new UsersLoginsViewModel(_eventAggregator, user, false));
			}
		}

		#endregion
	}
}
