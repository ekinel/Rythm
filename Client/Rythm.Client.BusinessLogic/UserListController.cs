// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic
{
    using System;
    using System.Collections.Generic;

    using Interfaces;

    public class UserListController : IUserListController
    {
        #region Fields

        private List<string> UpdatedUsersList;

        private readonly IChatPanelController _chatPanelController;
        public event Action<List<string>> UpdatedUsersListEvent;


        #endregion

        #region Constructors

        public UserListController(IChatPanelController chatPanelController)
        {
            _chatPanelController = chatPanelController;
            _chatPanelController.UpdatedUsersListEvent += HandleUpdatedUsersList;
        }

        #endregion

        #region Methods

        public void HandleUpdatedUsersList(List<string> updatedUsersList)
        {
            UpdatedUsersList = new List<string>(updatedUsersList);
            UpdatedUsersListEvent?.Invoke(UpdatedUsersList);
        }

        #endregion
    }
}
