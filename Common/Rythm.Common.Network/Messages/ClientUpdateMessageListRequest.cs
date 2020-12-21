// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
	using Enums;

	public class ClientUpdateMessageListRequest : BaseContainer
	{
		#region Fields

		private readonly DataBaseMessage Message;

		#endregion

		#region Constructors

		public ClientUpdateMessageListRequest(DataBaseMessage message)
		{
			MessageType = MsgType.ClientUpdateMessageListRequest;
			Message = message;
		}

		#endregion
	}
}
