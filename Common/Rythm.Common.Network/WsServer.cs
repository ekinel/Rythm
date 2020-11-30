// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    using Enums;

    using Messages;

    using Newtonsoft.Json.Linq;

    using WebSocketSharp.Server;

    public class WsServer
    {
        #region Fields

        private readonly IPEndPoint _listenAddress;
        private readonly ConcurrentDictionary<string, WsConnection> _connections;

        private WebSocketServer _server;

        #endregion

        #region Events

        public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        #endregion

        #region Constructors

        public WsServer(IPEndPoint listenAddress)
        {
            _listenAddress = listenAddress;
            _connections = new ConcurrentDictionary<string, WsConnection>();
        }

        #endregion

        #region Methods

        public void Start()
        {
            _server = new WebSocketServer(_listenAddress.Address, _listenAddress.Port, false);
            //_server.AddWebSocketService("/", () => new WsConnection(this));
            _server.AddWebSocketService<WsConnection>(
                "/",
                client =>
                {
                    client.AddServer(this);
                });
            _server.Start();
        }

        public void Stop()
        {
            _server?.Stop();
            _server = null;

            WsConnection[] connections = _connections.Select(item => item.Value).ToArray();
            foreach (WsConnection connection in connections)
            {
                connection.Close();
            }

            _connections.Clear();
        }

        public void Send(string message)
        {
            MessageContainer messageBroadcast = new MessageBroadcast(message).GetContainer();

            foreach (KeyValuePair<string, WsConnection> connection in _connections)
            {
                connection.Value.Send(messageBroadcast);
            }
        }

        public void Send(TextMsgContainer MsgContainer)
        {
            MessageContainer messageBroadcast = new MessageBroadcast(MsgContainer.Message).GetContainer();

            foreach (KeyValuePair<string, WsConnection> connection in _connections)
            {
                if (connection.Value.Login == MsgContainer.To || connection.Value.Login == MsgContainer.From)
                {
                    connection.Value.Send(messageBroadcast);
                }
            }
        }

        internal void HandleMessage(WsConnection connection, MessageContainer container)
        {
            switch (container.Identifier)
            {
                case nameof(ConnectionRequest):
                    var connectionRequest = ((JObject)container.Payload).ToObject(typeof(ConnectionRequest)) as ConnectionRequest;
                    var connectionResponse = new ConnectionResponse
                    {
                        Result = ResultCodes.Ok
                    };
                    if (_connections.Values.Any(item => item.Login == connectionRequest.Login))
                    {
                        connectionResponse.Result = ResultCodes.Failure;
                        connectionResponse.Reason = $"Клиент с именем '{connectionRequest.Login}' уже подключен.";
                        connection.Send(connectionResponse.GetContainer());
                    }
                    else
                    {
                        connection.Login = connectionRequest.Login;
                        _connections.TryAdd(connection.Login, connection);

                        connection.Send(connectionResponse.GetContainer());
                        ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(connection.Login, true));
                    }

                    break;
                case nameof(MessageRequest):
                    var messageRequest = ((JObject)container.Payload).ToObject(typeof(MessageRequest)) as MessageRequest;
                    MessageReceived?.Invoke(this, new MessageReceivedEventArgs(messageRequest.MsgContainer));
                    break;
            }
        }

        internal void FreeConnection(string login)
        {
            if (_connections.TryRemove(login, out WsConnection connection) && !string.IsNullOrEmpty(connection.Login))
            {
                ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(connection.Login, false));
            }
        }

        #endregion
    }
}
