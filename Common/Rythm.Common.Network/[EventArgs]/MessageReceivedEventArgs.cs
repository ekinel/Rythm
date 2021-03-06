﻿// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network
{
	using System;

	using Messages;
	using Rythm.Common.Network.Enums;

	public class MessageReceivedEventArgs
    {
        #region Properties

        public string ToClientName { get; }

        public string Message { get; }

        public string FromClientName { get; }

        public DateTime Date { get; }

        public MsgStatus Status { get; }

        #endregion

        #region Constructors

        public MessageReceivedEventArgs(TextMsgRequest msgRequest)
        {
            ToClientName = msgRequest.To;
            Message = msgRequest.Message;
            FromClientName = msgRequest.From;
            Date = msgRequest.Date;
            Status = msgRequest.Status;
        }

        #endregion
    }
}
