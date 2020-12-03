// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic
{
    using System;
    using System.Collections.Generic;

    using Common.Network;
    using Common.Network.Messages;

    public interface IChatPanelController
    {
        #region Events

        event Action<MessageReceivedEventArgs> MessageReceivedEvent;
        event Action<List<string>> UpdatedUsersListEvent;
        event Action<ServerOkMsgResponse> ServerOkReceivedEvent;
        event Action<ClientOkMsgResponse> ClientOkReceivedEvent;

        #endregion

        #region Methods

        void MessageSend(string currentMessage, string loginFrom);

        #endregion
    }
}
