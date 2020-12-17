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

		public readonly List<KeyValuePair<string, string>> EventsList;

		#endregion

		#region Constructors

		public UpdatedDataBaseEvents(List<string> eventsList, List<string> dateList)
		{
			MessageType = MsgType.UpdatedDataBaseEvents;
			EventsList = new List<KeyValuePair<string, string>>();

			for (int i = 0; i < eventsList.Count; i++)
			{
				EventsList.Add(new KeyValuePair<string, string>(eventsList[i], dateList[i]));
			}
		}

		#endregion
	}
}
