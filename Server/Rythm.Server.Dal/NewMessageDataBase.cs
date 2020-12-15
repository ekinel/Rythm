﻿// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Dal
{
	using System.ComponentModel.DataAnnotations;

	using Common.Network.Enums;

	public class NewMessageDataBase
	{
		#region Properties

		[Key]
		public string Date { get; set; }

		public MsgType MessageType { get; set; }
		public string Message { get; set; }
		public string ClientTo { get; set; }
		public string ClientFrom { get; set; }

		#endregion
	}
}