// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network
{
    public class MessageReceivedEventArgs
    {
        #region Properties

        public string ToClientName { get; }

        public string Message { get; }

        public string FromClientName { get; }

        public string Date { get; }

        #endregion

        #region Constructors

        public MessageReceivedEventArgs(TextMsgContainer msgContainer)
        {
            ToClientName = msgContainer.To;
            Message = msgContainer.Message;
            FromClientName = msgContainer.From;
            Date = msgContainer.Date;
        }

        #endregion
    }
}
