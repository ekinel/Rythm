﻿using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;
using Rythm.Common.Network.Enums;
using Rythm.Common.Network.Events;
using Rythm.Common.Network.Messages;
using WebSocketSharp.Server;

namespace Rythm.Common.Network
{
    public class WsServer
    {
        #region Constructors

        public WsServer(IPEndPoint listenAddress)
        {
            _listenAddress = listenAddress;
            _connections = new ConcurrentDictionary<Guid, WsConnection>();
        }

        #endregion Constructors

        #region Fields

        private readonly IPEndPoint _listenAddress;
        private readonly ConcurrentDictionary<Guid, WsConnection> _connections;

        private WebSocketServer _server;

        #endregion Fields

        #region Events

        public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        #endregion Events

        #region Methods

        public void Start()
        {
            _server = new WebSocketServer(_listenAddress.Address, _listenAddress.Port, false);
            //_server.AddWebSocketService("/", () => new WsConnection(this));
            _server.AddWebSocketService<WsConnection>("/",
                client => { client.AddServer(this); });
            _server.Start();
        }

        public void Stop()
        {
            _server?.Stop();
            _server = null;

            var connections = _connections.Select(item => item.Value).ToArray();
            foreach (var connection in connections) connection.Close();

            _connections.Clear();
        }

        public void Send(string message)
        {
            var messageBroadcast = new MessageBroadcast(message).GetContainer();

            foreach (var connection in _connections) connection.Value.Send(messageBroadcast);
        }

        internal void HandleMessage(Guid clientId, MessageContainer container)
        {
            if (!_connections.TryGetValue(clientId, out var connection))
                return;

            switch (container.Identifier)
            {
                case nameof(ConnectionRequest):
                    var connectionRequest =
                        ((JObject) container.Payload).ToObject(typeof(ConnectionRequest)) as ConnectionRequest;
                    var connectionResponse = new ConnectionResponse {Result = ResultCodes.Ok};
                    if (_connections.Values.Any(item => item.Login == connectionRequest.Login))
                    {
                        connectionResponse.Result = ResultCodes.Failure;
                        connectionResponse.Reason = $"Клиент с именем '{connectionRequest.Login}' уже подключен.";
                        connection.Send(connectionResponse.GetContainer());
                    }
                    else
                    {
                        connection.Login = connectionRequest.Login;
                        connection.Send(connectionResponse.GetContainer());
                        ConnectionStateChanged?.Invoke(this,
                            new ConnectionStateChangedEventArgs(connection.Login, true));
                    }

                    break;
                case nameof(MessageRequest):
                    var messageRequest =
                        ((JObject) container.Payload).ToObject(typeof(MessageRequest)) as MessageRequest;
                    MessageReceived?.Invoke(this,
                        new MessageReceivedEventArgs(connection.Login, messageRequest.Message));
                    break;
            }
        }

        internal void AddConnection(WsConnection connection)
        {
            _connections.TryAdd(connection.Id, connection);
        }

        internal void FreeConnection(Guid connectionId)
        {
            if (_connections.TryRemove(connectionId, out var connection) && !string.IsNullOrEmpty(connection.Login))
                ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(connection.Login, false));
        }

        #endregion Methods
    }
}