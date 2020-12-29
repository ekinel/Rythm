﻿// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Dal
{
	using System;
	public class MessageDTO
	{
		#region Properties

		public int ID { get; set; }
		public DateTime Date { get; set; }
		public string Message { get; set; }
		public string ClientTo { get; set; }
		public string ClientFrom { get; set; }
		public string MsgStatus { get; set; }

		#endregion
	}
}