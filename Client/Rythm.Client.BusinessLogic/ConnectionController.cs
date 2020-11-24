// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic
{
    using Common.Network;

    public class ConnectionController : IConnectionController
    {
        #region Properties

        public ITransport CurrentTransport { get; }

        public string Login { get; set; }

        #endregion

        #region Constructors

        public ConnectionController()
        {
            CurrentTransport = TransportFactory.Create(TransportType.WebSocket);
            CurrentTransport.ConnectionStateChanged += HandleConnectionStateChanged;
        }

        #endregion

        #region Methods

        public void DataSending(string address, string port, string login)
        {
            CurrentTransport.Connect(address, port);
            Login = login;
        }

        private void HandleConnectionStateChanged(object sender, ConnectionStateChangedEventArgs state)
        {
            ConnectionStateChanged(state);
        }

        private void ConnectionStateChanged(ConnectionStateChangedEventArgs state)
        {
            if (state.Connected)
            {
                if (string.IsNullOrEmpty(state.ClientName))
                {
                    CurrentTransport?.Login(Login);
                }
            }
        }

        #endregion
    }
}
