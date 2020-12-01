// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
    using Enums;

    public class ConnectionRequest : BaseContainer
    {
        #region Properties

        public string Login { get; }

        #endregion

        #region Constructors

        public ConnectionRequest(string login)
        {
            Login = login;
            MessageType = MsgType.ClientRegistration;
        }

        #endregion
    }
}
