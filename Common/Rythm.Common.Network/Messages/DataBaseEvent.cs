// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
	using System;

	public class DataBaseEvent : BaseContainer
	{
		public string Message;
		public DateTime Date;

		public DataBaseEvent(string message, DateTime date)
		{
			Message = message;
			Date = date;
		}
	}
}
