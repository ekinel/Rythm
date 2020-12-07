// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network
{
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
        private bool _isCommonChatCreated = true;

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

        internal void HandleMessage(WsConnection connection, MessageContainer container)
        {
            switch (container.Identifier)
            {
                case MsgType.ClientRegistration:
                    var connectionRequest = ((JObject)container.Payload).ToObject(typeof(ConnectionRequest)) as ConnectionRequest;
                    var connectionResponse = new ConnectionResponse
                    {
                        Result = ResultCodes.Ok
                    };

                    if (connectionRequest != null)
                    {
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
                            var updatedClientsList = new UpdatedClientsResponse(_connections.Keys);
                            Send(updatedClientsList);

                            if (_isCommonChatCreated)
                            {
                                _connections.TryAdd("CommonChat", connection);
                                _isCommonChatCreated = !_isCommonChatCreated;
                                var updateClientsList = new UpdatedClientsResponse(_connections.Keys);
                                updateClientsList.UsersList.Remove(connection.Login);
                                Send(updateClientsList);
                            }
                        }
                    }

                    break;

                case MsgType.PersonalMessage:
                    var messageRequest = ((JObject)container.Payload).ToObject(typeof(MessageRequest)) as MessageRequest;

                    if (((JObject)messageRequest?.MsgContainer)?.ToObject(typeof(TextMsgRequest)) is TextMsgRequest textMsgContainer)
                    {
                        var serverOkPayload = new ServerOkMsgResponse(textMsgContainer.From, textMsgContainer.To, textMsgContainer.Date);
                        MessageContainer serverOkContainer = serverOkPayload.GetContainer();

                        if (textMsgContainer.To == "CommonChat")
                        {
                            BroadcastSend(container, textMsgContainer.From);
                        }
                        else
                        {
                            Send(container, textMsgContainer.To);
                            Send(serverOkContainer, textMsgContainer.From);
                        }
                    }

                    break;

                case MsgType.ClientOk:

                    if (((JObject)container.Payload).ToObject(typeof(ClientOkMsgResponse)) is ClientOkMsgResponse clientOkMsgContainer)
                    {
                        MessageContainer clientOkContainer = clientOkMsgContainer.GetContainer();

                        Send(clientOkContainer, clientOkMsgContainer.From);
                    }

                    break;
            }
        }

        internal void FreeConnection(string login)
        {
            if (_connections.TryRemove(login, out WsConnection connection) && !string.IsNullOrEmpty(connection.Login))
            {
                Send(new UpdatedClientsResponse(_connections.Keys));
            }
        }

        private void BroadcastSend(MessageContainer msgContainer, string loginFrom)
        {
            foreach (KeyValuePair<string, WsConnection> connection in _connections)
            {
                if (connection.Value.Login != loginFrom && connection.Key != "CommonChat")
                {
                    connection.Value.Send(msgContainer);
                }
            }
        }

        private void Send(MessageContainer msgContainer, string targetId)
        {
            foreach (KeyValuePair<string, WsConnection> connection in _connections)
            {
                if (connection.Value.Login == targetId)
                {
                    connection.Value.Send(msgContainer);
                }
            }
        }

        private void Send(UpdatedClientsResponse updatedClientsResponse)
        {
            foreach (KeyValuePair<string, WsConnection> connection in _connections)
            {
                ICollection<string> updatedClientsList = new List<string>(updatedClientsResponse.UsersList);
                updatedClientsList.Remove(connection.Key);
                var newUpdatedClientsResponse = new UpdatedClientsResponse(updatedClientsList);
                connection.Value.Send(newUpdatedClientsResponse.GetContainer());
            }
        }

        #endregion
    }
}
