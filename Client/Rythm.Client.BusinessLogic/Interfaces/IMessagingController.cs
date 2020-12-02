// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic
{
    using System;

    public interface IMessagingController
    {
        #region Events

        event Action<string> MessageReceivedEvent;
        event Action<string> UserLoginEvent;

        #endregion

        #region Methods

        void MessageSend(string currentMessage);

        #endregion
    }
}
