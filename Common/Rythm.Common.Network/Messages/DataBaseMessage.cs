// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
	using System;

	public class DataBaseMessage
	{
		#region Properties

		public string Text;
		public DateTime Date;
		public string ClientFrom;
		public string ClientTo;

		#endregion

		#region Constructors

		public DataBaseMessage(string text, DateTime date, string clientFrom, string clientTo)
		{
			Text = text;
			Date = date;
			ClientFrom = clientFrom;
			ClientTo = clientTo;
		}

		#endregion
	}
}
