// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic.Interfaces
{
	using System;
	using System.Collections.Generic;

	using Common.Network;
	using Common.Network.Enums;

	public interface IChatPanelController
	{
		#region Events

		event Action<MessageReceivedEventArgs> MessageReceivedEvent;
		event Action<List<string>, List<string>> UpdatedUsersListEvent;
		event Action<(MsgStatus, DateTime)> OkReceivedEvent;

		#endregion

		#region Methods

		void MessageSend(string currentMessage, string loginFrom);
		#endregion
	}
}
