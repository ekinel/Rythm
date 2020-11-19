namespace Rythm.Common.Network
{
    using System;
    public interface ITransport
    {
        #region Events

        event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
        event EventHandler<MessageReceivedEventArgs> MessageReceived;

        #endregion Events

        #region Methods

        void Connect(string address, string port);

        void Disconnect();

        void Login(string login);

        void Send(string message);

        #endregion Methods
    }
}
