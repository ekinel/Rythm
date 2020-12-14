// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network
{
	using Messages;

	public class MessageReceivedEventArgs
    {
        #region Properties

        public string ToClientName { get; }

        public string Message { get; }

        public string FromClientName { get; }

        public string Date { get; }

        #endregion

        #region Constructors

        public MessageReceivedEventArgs(TextMsgRequest msgRequest)
        {
            ToClientName = msgRequest.To;
            Message = msgRequest.Message;
            FromClientName = msgRequest.From;
            Date = msgRequest.Date;
        }

        #endregion
    }
}
