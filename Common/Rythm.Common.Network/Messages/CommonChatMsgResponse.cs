// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
    using System;

    using Enums;

    internal class CommonChatMsgResponse : BaseContainer
    {
        #region Properties

        public DateTime Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Message { get; set; }

        #endregion

        #region Constructors

        public CommonChatMsgResponse(string from, string to, string message, DateTime date)
        {
            From = from;
            To = to;
            Message = message;
            Date = date;
            MessageType = MsgType.CommonMessage;
        }

        #endregion
    }
}
