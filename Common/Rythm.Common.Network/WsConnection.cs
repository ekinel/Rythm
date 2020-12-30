// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network
{
    using System.Collections.Concurrent;
    using System.Threading;

    using Messages;

    using Newtonsoft.Json;

    using WebSocketSharp;
    using WebSocketSharp.Server;

    public class WsConnection : WebSocketBehavior
    {
        #region Fields

        private readonly ConcurrentQueue<MessageContainer> _sendQueue;

        private WsServer _server;

        private int _sending;

        #endregion

        #region Properties

        public string Login { get; set; } = string.Empty;

        private bool IsConnected => Context.WebSocket?.ReadyState == WebSocketState.Open;

        #endregion

        #region Constructors

        public WsConnection()
        {
            _sendQueue = new ConcurrentQueue<MessageContainer>();
            _sending = 0;
        }

        #endregion

        #region Methods

        public void AddServer(WsServer server)
        {
            _server = server;
        }

        public void Send(MessageContainer container)
        {
            if (!IsConnected)
            {
                return;
            }

            _sendQueue.Enqueue(container);
            if (Interlocked.CompareExchange(ref _sending, 1, 0) == 0)
            {
                SendImpl();
            }
        }

        public void Close()
        {
            Context.WebSocket.Close();
        }

        protected override void OnClose(CloseEventArgs e)
        {
            _server.FreeConnection(Login);
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            if (e.IsText)
            {
                var message = JsonConvert.DeserializeObject<MessageContainer>(e.Data);
                _server.HandleMessage(this, message);
            }
        }

        private void SendCompleted(bool completed)
        {
            if (!completed)
            {
                _server.FreeConnection(Login);
                Context.WebSocket.CloseAsync();
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
            SendAsync(serializedMessages, SendCompleted);
        }

        #endregion
    }
}
