// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network
{
    using System;

    public class TextMsgContainer
    {
        #region Fields

        private string _from;
        private string _to;
        private string _message;
        private string _date;
        private MsgType messageType { get; set; } = MsgType.Personal;

        #endregion

        #region Properties
        public MsgType MessageType
        {
            get => messageType;
            set => messageType = value;
        }
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

        public enum MsgType
        {
            Group,
            Personal
        }

        #endregion

        #region Constructors

        public TextMsgContainer(string from, string to, string message)
        {
            From = from;
            To = to;
            Message = message;
            Date = DateTime.Now.ToString();
            MessageType = MsgType.Personal;
        }

        #endregion
    }
}
