// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic.Interfaces
{
	using System;
	using System.Collections.Generic;

	public interface IUsersListController
	{
		#region Events

		event Action<List<string>, List<string>> UpdatedUsersListEvent;

		#endregion
	}
}
