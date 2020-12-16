// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
	using System.Collections.Generic;

	using Enums;

	public class UpdatedDataBaseMessages : BaseContainer
	{
		#region Fields

		public readonly List<string> _msgDate;
		public readonly List<string> _msgFrom;
		public readonly List<string> _msgTo;
		public readonly List<string> _msgText;

		#endregion

		#region Constructors

		public UpdatedDataBaseMessages(List<string> date, List<string> from, List<string> to, List<string> message)
		{
			MessageType = MsgType.UpdatedDataBaseMessages;
			_msgDate = message;
			_msgFrom = from;
			_msgTo = to;
			_msgText = message;
		}

		#endregion
	}
}
