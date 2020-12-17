// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
	using System.Collections.Generic;

	using Enums;

	public class UpdatedDataBaseEvents : BaseContainer
	{
		#region Fields

		public readonly List<DataBaseEvent> EventsList = new List<DataBaseEvent>();

		#endregion

		#region Constructors

		public UpdatedDataBaseEvents(List<DataBaseEvent> eventsList)
		{
			MessageType = MsgType.UpdatedDataBaseEvents;

			foreach (var element in eventsList)
			{
				EventsList.Add(element);
			}
		}

		#endregion
	}
}
