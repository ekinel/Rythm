// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
	using System.Collections.Generic;

	using Enums;

	using Server.Dal;

	public class UpdatedDataBaseEvents : BaseContainer
	{
		#region Fields

		public readonly List<string> EventsList;
		public readonly List<string> DateList;

		#endregion

		#region Constructors

		public UpdatedDataBaseEvents(List<string> eventsList, List<string> dateList)
		{
			MessageType = MsgType.UpdatedDataBaseEvents;
			EventsList = eventsList;
			DateList = dateList;
		}

		#endregion
	}
}
