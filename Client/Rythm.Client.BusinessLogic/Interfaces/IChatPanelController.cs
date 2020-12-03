// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic
{
    using System;
    using System.Collections.Generic;

    using Common.Network;

    public interface IChatPanelController
    {
        #region Events

        event Action<MessageReceivedEventArgs> MessageReceivedEvent;
        event Action<List<string>> UpdatedUsersListEvent;

        #endregion

        #region Methods

        void MessageSend(string currentMessage, string loginFrom);

        #endregion
    }
}
