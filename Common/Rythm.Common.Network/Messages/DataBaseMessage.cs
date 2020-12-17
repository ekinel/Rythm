// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
	public class DataBaseMessage
	{
		#region Properties

		public string Text { get; }
		public string Date { get; }
		public string ClientFrom { get; }
		public string ClientTo { get; }

		#endregion

		#region Constructors

		public DataBaseMessage(string text, string date, string clientFrom, string clientTo)
		{
			Text = text;
			Date = date;
			ClientFrom = clientFrom;
			ClientTo = clientTo;
		}

		#endregion
	}
}
