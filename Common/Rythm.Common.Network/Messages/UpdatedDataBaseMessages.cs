// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
	using System.Collections.Generic;

	using Enums;

	public class UpdatedDataBaseMessages : BaseContainer
	{
		#region Properties

		public readonly List<DataBaseMessage> MessagesList = new List<DataBaseMessage>();

		#endregion

		#region Constructors

		public UpdatedDataBaseMessages(List<DataBaseMessage> messagesList)
		{
			MessageType = MsgType.UpdatedDataBaseMessages;

			foreach (var element in messagesList)
			{
				MessagesList.Add(element);
			}
			MessagesList = messagesList;
		}

		#endregion
	}
}
