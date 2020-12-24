// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
	using Rythm.Common.Network.Enums;
	using System;

	public class DataBaseMessage
	{
		#region Properties

		public string Text;
		public DateTime Date;
		public string ClientFrom;
		public string ClientTo;
		public string MsgStatus; 

		#endregion

		#region Constructors

		public DataBaseMessage(string text, DateTime date, string clientFrom, string clientTo, string msgStatus)
		{
			Text = text;
			Date = date;
			ClientFrom = clientFrom;
			ClientTo = clientTo;
			MsgStatus = msgStatus;
		}

		#endregion
	}
}
