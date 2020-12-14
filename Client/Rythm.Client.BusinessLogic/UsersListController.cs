// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic
{
	using System;
	using System.Collections.Generic;

	using Interfaces;

	public class UsersListController : IUsersListController
	{
		#region Fields

		private readonly IChatPanelController _chatPanelController;

		#endregion

		#region Events

		public event Action<List<string>, List<string>> UpdatedUsersListEvent;

		#endregion

		#region Constructors

		public UsersListController(IChatPanelController chatPanelController)
		{
			_chatPanelController = chatPanelController;
			_chatPanelController.UpdatedUsersListEvent += HandleUpdatedUsersList;
		}

		#endregion

		#region Methods

		public void HandleUpdatedUsersList(List<string> activeUsersList, List<string> notActiveUsersList)
		{
			UpdatedUsersListEvent?.Invoke(activeUsersList, notActiveUsersList);
		}

		#endregion
	}
}
