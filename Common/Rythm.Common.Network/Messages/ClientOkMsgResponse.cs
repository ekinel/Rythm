// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
	using System;

	using Enums;

	public class ClientOkMsgResponse : BaseContainer
    {
        #region Fields

        private string _from;
        private string _to;

        #endregion

        #region Properties

        public string From
        {
            get => _from;
            set => _from = value ?? throw new ArgumentNullException(nameof(value));
        }

        public string To
        {
            get => _to;
            set => _to = value ?? throw new ArgumentNullException(nameof(value));
        }

        public DateTime Date { get; set; }

        #endregion

        #region Constructors

        public ClientOkMsgResponse(string from, string to, DateTime date)
        {
            From = from;
            To = to;
            Date = date;
            MessageType = MsgType.ClientOk;
        }

        #endregion
    }
}
