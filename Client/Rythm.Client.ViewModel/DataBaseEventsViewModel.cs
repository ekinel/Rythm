// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
	using System;

	public class DataBaseEventsViewModel
	{
		#region Properties

		public string EventMessage { get; }
		public DateTime EventDate { get; }

		#endregion

		#region Constructors

		public DataBaseEventsViewModel(string eventMessage, DateTime date)
		{
			EventMessage = eventMessage;
			EventDate = date;
		}

		#endregion
	}
}
