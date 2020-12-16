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

		#endregion
	}
}
