// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
	using Rythm.Common.Network.Enums;
	using System;

	public class OkReceiveMsg
	{
		public MsgStatus MsgStatus { get; set; }
		public DateTime MsgTime { get; set; }

		public OkReceiveMsg(MsgStatus msgStatus, DateTime msgTime)
		{
			MsgStatus = msgStatus;
			MsgTime = msgTime;
		}
	}
}
