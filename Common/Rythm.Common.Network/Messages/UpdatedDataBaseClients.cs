// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
	using System.Collections.Generic;

	using Enums;

	public class UpdatedDataBaseClients : BaseContainer
	{
		#region Fields

		public readonly List<string> ClientsList;

		#endregion

		#region Constructors

		public UpdatedDataBaseClients(List<string> clientsList)
		{
			MessageType = MsgType.UpdatedDataBaseClients;
			ClientsList = clientsList;
		}

		#endregion
	}
}
