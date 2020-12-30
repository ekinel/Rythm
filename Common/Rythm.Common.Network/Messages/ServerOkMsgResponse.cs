// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
	using System;

	using Enums;

    public class ServerOkMsgResponse : BaseContainer
    {
        #region Properties

        public DateTime Date { get; set; }
        private string From { get; set; }
        private string To { get; set; }

        #endregion

        #region Constructors

        public ServerOkMsgResponse(string from, string to, DateTime date)
        {
            From = from;
            To = to;
            Date = date;
            MessageType = MsgType.ServerOk;
        }

        #endregion
    }
}
