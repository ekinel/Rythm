// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
	using Rythm.Common.Network.Enums;
	using System;

	public class TextMsgRequest
    {
        #region Properties
        public string From { get; set; }
		public string To { get; set; }
        public string Message { get; set; }
        public MsgStatus Status { get; set; }
        public DateTime Date { get; set; }

        #endregion

        #region Constructors

        public TextMsgRequest(string from, string to, string message, MsgStatus status)
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
