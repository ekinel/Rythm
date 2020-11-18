﻿namespace Rythm.Common.Network.Events
{
    public class MessageReceivedEventArgs
    {
        #region Properties

        public string ClientName { get; }

        public string Message { get; }

        #endregion Properties

        #region Constructors

        public MessageReceivedEventArgs(string clientName, string message)
        {
            ClientName = clientName;
            Message = message;
        }

        #endregion Constructors
    }
}
