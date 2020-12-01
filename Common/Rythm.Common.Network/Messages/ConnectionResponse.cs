// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
    using Enums;

    public class ConnectionResponse : BaseContainer
    {
        #region Properties

        public ResultCodes Result { get; set; }

        public string Reason { get; set; }

        #endregion

        #region Constructors

        public ConnectionResponse()
        {
            MessageType = MsgType.ClientRegistration;
        }

        #endregion
    }
}
