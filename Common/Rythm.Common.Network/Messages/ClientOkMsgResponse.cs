// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
	using System;

	using Enums;

	public class ClientOkMsgResponse : BaseContainer
    {
        #region Properties

        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }

        #endregion

        #region Constructors

        public ClientOkMsgResponse(string from, string to, DateTime date)
        {
            From = from;
            To = to;
            Date = date;
            MessageType = MsgType.ClientOk;
        }

        #endregion
    }
}
