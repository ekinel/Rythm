// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic.Interfaces
{
	using System;
	using System.Collections.Generic;

	using Common.Network;
	using Rythm.Common.Network.Messages;

	public interface IChatPanelController
	{
		#region Events

		event Action<MessageReceivedEventArgs> MessageReceivedEvent;
		event Action<List<string>, List<string>> UpdatedUsersListEvent;
		event Action<OkReceiveMsg> OkReceivedEvent;

		#endregion

		#region Methods

		void MessageSend(string currentMessage, string loginFrom);
		#endregion
	}
}
