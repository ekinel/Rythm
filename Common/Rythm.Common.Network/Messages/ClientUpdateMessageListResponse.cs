// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
	using System.Collections.Generic;

	using Enums;

	internal class ClientUpdateMessageListResponse : BaseContainer
	{
		#region Fields

		public List<DataBaseMessage> Messages;

		#endregion

		#region Constructors

		public ClientUpdateMessageListResponse(List<DataBaseMessage> messages)
		{
			MessageType = MsgType.ClientUpdateMessageListResponse;
			Messages = messages;
		}

		#endregion
	}
}
