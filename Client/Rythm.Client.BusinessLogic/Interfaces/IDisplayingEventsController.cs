// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic.Interfaces
{
	using System;
	using System.Collections.Generic;

	public interface IDisplayingEventsController
	{
		#region Events

		event Action<List<string>> UpdatedDataBaseClientsEvent;
		event Action<List<string>, List<string>, List<string>, List<string>> UpdatedDataBaseMessagesEvent;
		event Action<List<string>, List<string>> UpdatedDataBaseEventsEvent;

		#endregion
	}
}
