// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic.Interfaces
{
	using System;
	using System.Collections.Generic;

	using Common.Network.Messages;

	public interface IDisplayingEventsController
	{
		#region Events

		event Action<List<string>> UpdatedDataBaseClientsEvent;
		event Action<List<DataBaseMessage>> UpdatedDataBaseMessagesEvent;
		event Action<List<DataBaseEvent>> UpdatedDataBaseEventsEvent;

		#endregion
	}
}
