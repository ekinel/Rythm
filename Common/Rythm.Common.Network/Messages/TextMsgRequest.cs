// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
	using System;

	public class TextMsgRequest
    {
        #region Fields

        private string _from;
        private string _to;
        private string _message;
        private string _status;

        #endregion

        #region Properties

        public string From
        {
            get => _from;
            private set => _from = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string To
        {
            get => _to;
            private set => _to = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string Message
        {
            get => _message;
            private set => _message = value ?? throw new ArgumentNullException(nameof(value));
        }
        public string Status
        {
            get => _status;
            private set => _status = value ?? throw new ArgumentNullException(nameof(value));
        }

        public DateTime Date { get; set; }

        #endregion

        #region Constructors

        public TextMsgRequest(string from, string to, string message, string status)
        {
            From = from;
            To = to;
            Message = message;
            Date = DateTime.Now;
			Status = status;
        }

        #endregion
    }
}
