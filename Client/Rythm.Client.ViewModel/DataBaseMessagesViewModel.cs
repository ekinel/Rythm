// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
	using Common.Network.Messages;

	public class DataBaseMessagesViewModel
	{
		#region Properties

		public string Text { get; }
		public string Date { get; }
		public string ClientFrom { get; }
		public string ClientTo { get; }

		#endregion

		#region Constructors

		public DataBaseMessagesViewModel(DataBaseMessage message)
		{
			Text = message.Text;
			Date = message.Date;
			ClientFrom = message.ClientFrom;
			ClientTo = message.ClientTo;
		}

		#endregion
	}
}
