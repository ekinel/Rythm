// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic
{
    using System;

    using Common.Network;

    public class ConnectionController : IConnectionController
    {
        #region Fields

        private string _connectionParametersViewSettingsVisibility;

        #endregion

        #region Properties

        public ITransport CurrentTransport { get; }
        public IUserLoginDisplayController UserLoginDisplayController { get; set; }

        public string Login { get; set; }

        public string ConnectionParametersViewVisibility
        {
            get => _connectionParametersViewSettingsVisibility;
            set
            {
                _connectionParametersViewSettingsVisibility = value ?? throw new ArgumentNullException(nameof(value));
                SendNewStateParametersViewVisibility(_connectionParametersViewSettingsVisibility);
            }
        }

        #endregion

        #region Events

        public event Action<string> SendNewStateParametersViewVisibility;

        #endregion

        #region Constructors

        public ConnectionController(IUserLoginDisplayController _UserLoginDisplayController)
        {
            CurrentTransport = TransportFactory.Create(TransportType.WebSocket);
            CurrentTransport.ConnectionStateChanged += HandleConnectionStateChanged;
            UserLoginDisplayController = _UserLoginDisplayController;
            UserLoginDisplayController.NewParametersViewVisibilityEvent += HandleConnectionParametersViewVisibility;
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

        private void HandleConnectionParametersViewVisibility(bool visibility)
        {
            if (visibility == false)
            {
                ConnectionParametersViewVisibility = "visibility";
            }
        }

        #endregion
    }
}
