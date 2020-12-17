// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
	public class DataBaseEventsViewModel
	{
		#region Properties

		public string EventMessage { get; }
		public string EventDate { get; }

		#endregion

		#region Constructors

		public DataBaseEventsViewModel(string eventMessage, string date)
		{
			EventMessage = eventMessage;
			EventDate = date;
		}

		#endregion
	}
}
