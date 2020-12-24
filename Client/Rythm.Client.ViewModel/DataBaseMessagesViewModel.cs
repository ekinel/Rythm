// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
	using System;

	using Common.Network.Messages;

	public class DataBaseMessagesViewModel
	{
		#region Properties

		public string Text { get; }
		public DateTime Date { get; }
		public string ClientFrom { get; }
		public string ClientTo { get; }
		public string Status { get; }

		#endregion

		#region Constructors

		public DataBaseMessagesViewModel(DataBaseMessage message)
		{
			Text = message.Text;
			Date = message.Date;
			ClientFrom = message.ClientFrom;
			ClientTo = message.ClientTo;
			Status = message.MsgStatus;
		}

		#endregion
	}
}
