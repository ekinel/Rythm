// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
    using System;

    using Enums;

    internal class CommonChatMsgResponse : BaseContainer
    {
        #region Fields

        private string _date;
        private string _from;
        private string _to;
        private string _message;

        #endregion

        #region Properties

        public string From
        {
            get => _from;
            set => _from = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string To
        {
            get => _to;
            set => _to = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Message
        {
            get => _message;
            set => _message = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Date
        {
            get => _date;
            set => _date = value ?? throw new ArgumentNullException(nameof(value));
        }

        #endregion

        #region Constructors

        public CommonChatMsgResponse(string from, string to, string message, string date)
        {
            From = from;
            To = to;
            Message = message;
            Date = date;
            MessageType = MsgType.GroupMessage;
        }

        #endregion
    }
}
