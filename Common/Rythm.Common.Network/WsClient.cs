// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network
{
    using System;
    using System.Collections.Concurrent;
    using System.Net.WebSockets;
    using System.Threading;

    using Enums;

    using Messages;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using WebSocketSharp;

    using WebSocket = WebSocketSharp.WebSocket;
    using WebSocketState = WebSocketSharp.WebSocketState;

    public class WsClient : ITransport
    {
        #region Fields

        private readonly ConcurrentQueue<MessageContainer> _sendQueue;

        private WebSocket _socket;

        private int _sending;

        #endregion

        #region Properties

        public bool IsConnected => _socket?.ReadyState == WebSocketState.Open;

        #endregion

        #region Events

        public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;
        public event EventHandler<EventArgs> ConnectionOpened;

        public event EventHandler<MessageContainer> UpdatedUsersList;
        public event EventHandler<(MsgType, string)> OkReceive;

        #endregion

        #region Constructors

        public WsClient()
        {
            _sendQueue = new ConcurrentQueue<MessageContainer>();
            _sending = 0;
        }

        #endregion

        #region Methods

        public void Connect(string address, string port)
        {
            if (IsConnected)
            {
                Disconnect();
            }

            _socket = new WebSocket($"ws://{address}:{port}");
            _socket.OnOpen += OnOpen;
            _socket.OnClose += OnClose;
            _socket.OnMessage += OnMessage;
            _socket.ConnectAsync();
        }

        public void Disconnect()
        {
            if (_socket == null)
            {
                return;
            }

            if (IsConnected)
            {
                _socket.CloseAsync();
                ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(false));
            }

            _socket.OnOpen -= OnOpen;
            _socket.OnClose -= OnClose;
            _socket.OnMessage -= OnMessage;

            _socket = null;
        }

        public void Send(BaseContainer request)
        {
            _sendQueue.Enqueue(request.GetContainer());

            if (Interlocked.CompareExchange(ref _sending, 1, 0) == 0)
            {
                SendImpl();
            }
        }

        private void SendCompleted(bool completed)
        {
            if (!completed)
            {
                Disconnect();
                ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(false));
                return;
            }

            SendImpl();
        }

        private void SendImpl()
        {
            if (!IsConnected)
            {
                return;
            }

            if (!_sendQueue.TryDequeue(out MessageContainer message) && Interlocked.CompareExchange(ref _sending, 0, 1) == 1)
            {
                return;
            }

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            string serializedMessages = JsonConvert.SerializeObject(message, settings);
            _socket.SendAsync(serializedMessages, SendCompleted);
        }

        private void OnMessage(object sender, MessageEventArgs e)
        {
            if (!e.IsText)
            {
                return;
            }

            var container = JsonConvert.DeserializeObject<MessageContainer>(e.Data);

            switch (container.Identifier)
            {
                case MsgType.ClientRegistration:
                    var connectionResponse = ((JObject)container.Payload).ToObject(typeof(ConnectionResponse)) as ConnectionResponse;
                    if (connectionResponse.Result == ResultCodes.Failure)
                    {
                        return;
                    }

                    ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(true));

                    break;

                case MsgType.PersonalMessage:

                    var messageRequest = ((JObject)container.Payload).ToObject(typeof(MessageRequest)) as MessageRequest;
                    var textMsgRequest = ((JObject)messageRequest.MsgContainer).ToObject(typeof(TextMsgRequest)) as TextMsgRequest;

                    MessageReceived?.Invoke(this, new MessageReceivedEventArgs(textMsgRequest));
                    var msgContainer = new ClientOkMsgResponse(textMsgRequest.From, textMsgRequest.To, textMsgRequest.Date);
                    Send(msgContainer);

                    break;

                case MsgType.GroupMessage:

                    var msgRequest = ((JObject)container.Payload).ToObject(typeof(CommonChatMsgResponse)) as CommonChatMsgResponse;
                    var mess = new MessageReceivedEventArgs(new TextMsgRequest(msgRequest.From, "CommonChat", msgRequest.Message));
                    MessageReceived?.Invoke(this, mess);

                    //ClientOk еще посмотреть, тут логин местный надо
                    var mgContainer = new ClientOkMsgResponse(msgRequest.From, "CommonChat", msgRequest.Date);
                    Send(mgContainer);

                    break;


                case MsgType.ClientOk:
                case MsgType.ServerOk:

                    MsgType type = container.Identifier;
                    MsgType status;
                    string date;

                    if (type == MsgType.ServerOk)
                    {
                        var messageResponse = ((JObject)container.Payload).ToObject(typeof(ServerOkMsgResponse)) as ServerOkMsgResponse;
                        date = messageResponse.Date;
                        status = MsgType.ServerOk;
                    }
                    else
                    {
                        var messageResponse = ((JObject)container.Payload).ToObject(typeof(ClientOkMsgResponse)) as ClientOkMsgResponse;
                        date = messageResponse.Date;
                        status = MsgType.ClientOk;
                    }

                    OkReceive?.Invoke(this, (status, date));
                    break;

                case MsgType.UpdatedClientsList:
                    UpdatedUsersList?.Invoke(this, container);
                    break;
            }
        }

        private void OnClose(object sender, CloseEventArgs e)
        {
            ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(false));
        }

        private void OnOpen(object sender, EventArgs e)
        {
            ConnectionOpened?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
