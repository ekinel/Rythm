// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network
{
    using System;

    using Enums;

    using Messages;

    public interface ITransport
    {
        #region Events

        event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
        event EventHandler<EventArgs> ConnectionOpened;
        event EventHandler<MessageContainer> UpdatedUsersList;
        event EventHandler<(MsgType, DateTime)> OkReceive;
        event EventHandler<MessageContainer> UpdatedDataBaseData;

        #endregion

        #region Methods

        void Connect(string address, string port);

        void Disconnect();

        void Send(BaseContainer request);
        #endregion
    }
}
