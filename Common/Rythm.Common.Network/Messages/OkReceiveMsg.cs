// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
	using Rythm.Common.Network.Enums;
	using System;

	public class OkReceiveMsg
	{
		private MsgStatus _msgStatus { get; set; }
		private DateTime _msgTime { get; set; }

		public MsgStatus MsgStatus
		{
			get => _msgStatus;
			set => _msgStatus = value;
		}
		public DateTime MsgTime
		{
			get => _msgTime;
			set => _msgTime = value;
		}

		public OkReceiveMsg(MsgStatus msgStatus, DateTime msgTime)
		{
			MsgStatus = MsgStatus;
			MsgTime = msgTime;
		}
	}
}
