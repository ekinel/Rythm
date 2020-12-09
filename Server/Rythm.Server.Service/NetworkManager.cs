﻿// ---------------------------------------------------------------------------------------------------------------------------------------------------
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
        }

        public NetworkManager(string port)
        {
            int wsPort = Convert.ToInt32(port);
            _wsServer = new WsServer(new IPEndPoint(IPAddress.Any, wsPort));
        }

        public NetworkManager(string port, string address)
        {
            int wsPort = Convert.ToInt32(port);
            long wsAddress = long.Parse(address);
            _wsServer = new WsServer(new IPEndPoint(wsAddress, wsPort));
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

        #endregion
    }
}
