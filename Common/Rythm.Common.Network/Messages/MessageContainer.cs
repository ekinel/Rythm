// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
    using Enums;

    public class MessageContainer
    {
        #region Properties

        public MsgType Identifier { get; set; }

        public object Payload { get; set; }

        #endregion
    }
}
