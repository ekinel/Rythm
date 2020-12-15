// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Dal
{
	using Common.Network.Enums;

	public class NewMessageDataBase
	{
		#region Properties

		public int ID { get; set; }
		public string Date { get; set; }
		public MsgType MessageType { get; set; }
		public string Message { get; set; }
		public string ClientTo { get; set; }
		public string ClientFrom { get; set; }

		#endregion
	}
}
