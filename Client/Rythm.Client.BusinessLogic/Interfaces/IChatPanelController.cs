// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic
{
    using System;
    using System.Collections.Generic;

    public interface IChatPanelController
    {
        #region Events

        event Action<string> MessageReceivedEvent;
        event Action<List<string>> UpdatedUsersListEvent;


        #endregion

        #region Methods

        void MessageSend(string currentMessage, string loginFrom);

        #endregion
    }
}
