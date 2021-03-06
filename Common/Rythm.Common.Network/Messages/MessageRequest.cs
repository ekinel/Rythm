﻿// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
    using Enums;

    public class MessageRequest : BaseContainer
    {
        #region Properties

        public object MsgContainer { get; }

        #endregion

        #region Constructors

        public MessageRequest(object msgContainer, MsgType msgType)
        {
            MsgContainer = msgContainer;
            MessageType = msgType;
        }

        #endregion
    }
}
