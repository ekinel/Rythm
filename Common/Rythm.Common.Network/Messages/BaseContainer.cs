// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
    using Enums;

    public class BaseContainer
    {
        #region Properties

        protected MsgType MessageType { set; get; }

        #endregion

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = MessageType,
                Payload = this
            };

            return container;
        }

        #endregion
    }
}

