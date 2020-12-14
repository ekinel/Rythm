using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Common.Network.DataBase
{
	using Enums;

	public class NewMessageDataBase
	{
		private MsgType MessageType { get; set; }
		private string Message { get; set; }
		private string ClientTo { get; set; }
		private string ClientFrom { get; set; }
		private string Date { get; set; }

		public NewMessageDataBase(MsgType messageType, string message, string clientTo, string ckClientFrom, string date)
		{
			MessageType = messageType;
			Message = message;
			ClientTo = clientTo;
			ClientFrom = ClientFrom;
			Date = date;
		}
	}
}
