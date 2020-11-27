// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Service
{
    using System;
    using System.Net;

    using Common.Network;

    public class NetworkManager
    {
        #region Constants

        private const int WS_PORT = 65000;

        #endregion

        #region Fields

        private readonly WsServer _wsServer;

        #endregion

        #region Constructors

        public NetworkManager()
        {
            _wsServer = new WsServer(new IPEndPoint(IPAddress.Any, WS_PORT));
            _wsServer.ConnectionStateChanged += HandleConnectionStateChanged;
            _wsServer.MessageReceived += HandleMessageReceived;
        }

        #endregion

        #region Methods

        public void Start()
        {
            Console.WriteLine($"WebSocketServer: {IPAddress.Any}:{WS_PORT}");
            _wsServer.Start();
        }

        public void Stop()
        {
            _wsServer.Stop();
        }

        private void HandleMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var msgContainer = new TextMsgContainer(e.FromClientName, e.ToClientName, e.Message);
            _wsServer.Send(msgContainer);
        }

        private void HandleConnectionStateChanged(object sender, ConnectionStateChangedEventArgs e)
        {
            string clientState = e.Connected ? "подключен" : "отключен";
            string message = $"Клиент '{e.ClientName}' {clientState}.";

            Console.WriteLine(message);

            _wsServer.Send(message);
        }

        #endregion
    }
}
