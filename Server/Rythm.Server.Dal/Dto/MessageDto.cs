// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Dal.Dto
{
	using System;
	public class MessageDto
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
