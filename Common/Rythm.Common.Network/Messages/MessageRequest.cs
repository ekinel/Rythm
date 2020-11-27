namespace Rythm.Common.Network.Messages
{
    public class MessageRequest
    {
        #region Properties

        public TextMsgContainer MsgContainer { get; set; }

        public string Message { get; set; }
        #endregion Properties

        #region Constructors

        public MessageRequest(TextMsgContainer msgContainer)
        {
            MsgContainer = msgContainer;
        }

        //public MessageRequest(string message)
        //{
        //    Message = message;
        //}
        #endregion Constructors

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

        #endregion Methods
    }
}
