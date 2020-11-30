// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
    public class MessageRequest
    {
        #region Properties

        public TextMsgContainer MsgContainer { get; set; }

        #endregion

        #region Constructors

        public MessageRequest(TextMsgContainer msgContainer)
        {
            MsgContainer = msgContainer;
        }

        #endregion

        #region Methods

        public MessageContainer GetContainer()
        {
            var container = new MessageContainer
            {
                Identifier = nameof(MessageRequest),
                Payload = this
            };

            return container;
        }

        #endregion
    }
}
