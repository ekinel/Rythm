﻿// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
	using System.Collections.Generic;

	using Enums;

	public class UpdatedDataBaseMessages : BaseContainer
	{
		#region Properties

		public List<DataBaseMessage> MessagesList;

		#endregion

		#region Constructors

		public UpdatedDataBaseMessages(List<DataBaseMessage> messagesList)
		{
			MessageType = MsgType.UpdatedDataBaseMessages;
			MessagesList = messagesList;
		}

		#endregion
	}
}
