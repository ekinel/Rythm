﻿// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
	using System.Collections.Generic;
	using System.Collections.ObjectModel;

	using BusinessLogic.Interfaces;

	using Prism.Events;
	using Prism.Mvvm;
	using Rythm.Client.ViewModel.Events;

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
			if(eventAggregator != null && usersListController != null)
			{
				_eventAggregator = eventAggregator;
				usersListController.UpdatedUsersListEvent += HandleUpdatedUsersList;

				UserList = new ObservableCollection<UsersLoginsViewModel>();
			}
		}

		#endregion

		#region Methods

		private void HandleUpdatedUsersList(List<string> activeUsersList, List<string> notActiveUsersList)
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

			_eventAggregator.GetEvent<UpdateActiveClients>().Publish(activeUsersList);
		}

		#endregion
	}
}
