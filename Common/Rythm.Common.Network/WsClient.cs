// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network
{
	using System;
	using System.Collections.Concurrent;
	using System.Threading;

	using Enums;

	using Messages;

	using Newtonsoft.Json;
	using Newtonsoft.Json.Linq;

	using WebSocketSharp;

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
		public event EventHandler<MessageContainer> UpdatedDataBaseClients;
		public event EventHandler<MessageContainer> UpdatedDataBaseMessages;
		public event EventHandler<MessageContainer> UpdatedDataBaseEvents;
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

		private void OnMessage(object sender, MessageEventArgs message)
		{
			if (!message.IsText)
			{
				return;
			}

			var container = JsonConvert.DeserializeObject<MessageContainer>(message.Data);

			switch (container.Identifier)
			{
				case MsgType.ClientRegistration:
					HandleClientRegistration(container);
					break;

				case MsgType.PersonalMessage:
					HandlePersonalMessage(container);
					break;

				case MsgType.CommonMessage:
					HandleCommonMessage(container);
					break;

				case MsgType.ClientOk:
				case MsgType.ServerOk:
					HandleOkResponse(container);
					break;

				case MsgType.UpdatedClientsList:
					UpdatedUsersList?.Invoke(this, container);
					break;

				case MsgType.UpdatedDataBaseClients:
					UpdatedDataBaseClients?.Invoke(this, container);
					break;

				case MsgType.UpdatedDataBaseMessages:
					UpdatedDataBaseMessages?.Invoke(this, container);
					break;

				case MsgType.UpdatedDataBaseEvents:
					UpdatedDataBaseEvents?.Invoke(this, container);
					break;
			}
		}

		private void HandleClientRegistration(MessageContainer container)
		{
			var connectionResponse = ((JObject)container.Payload).ToObject(typeof(ConnectionResponse)) as ConnectionResponse;
			if (connectionResponse != null && connectionResponse.Result == ResultCodes.Failure)
			{
				return;
			}

			ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(true));
		}

		private void HandlePersonalMessage(MessageContainer container)
		{
			if (((JObject)container.Payload).ToObject(typeof(MessageRequest)) is MessageRequest messageRequest)
			{
				var textMsgRequest = ((JObject)messageRequest.MsgContainer).ToObject(typeof(TextMsgRequest)) as TextMsgRequest;

				MessageReceived?.Invoke(this, new MessageReceivedEventArgs(textMsgRequest));
				if (textMsgRequest != null)
				{
					var msgContainer = new ClientOkMsgResponse(textMsgRequest.From, textMsgRequest.To, textMsgRequest.Date);
					Send(msgContainer);
				}
			}
		}

		private void HandleCommonMessage(MessageContainer container)
		{
			var msgRequest = ((JObject)container.Payload).ToObject(typeof(CommonChatMsgResponse)) as CommonChatMsgResponse;
			if (msgRequest != null)
			{
				var mess = new MessageReceivedEventArgs(new TextMsgRequest(msgRequest.From, "CommonChat", msgRequest.Message));
				MessageReceived?.Invoke(this, mess);
			}

			if (msgRequest != null)
			{
				var mgContainer = new ClientOkMsgResponse(msgRequest.From, "CommonChat", msgRequest.Date);
				Send(mgContainer);
			}
		}

		private void HandleOkResponse(MessageContainer container)
		{
			MsgType type = container.Identifier;
			MsgType status;
			string date = string.Empty;

			if (type == MsgType.ServerOk)
			{
				if (((JObject)container.Payload).ToObject(typeof(ServerOkMsgResponse)) is ServerOkMsgResponse messageResponse)
				{
					date = messageResponse.Date;
				}

				status = MsgType.ServerOk;
			}
			else
			{
				if (((JObject)container.Payload).ToObject(typeof(ClientOkMsgResponse)) is ClientOkMsgResponse messageResponse)
				{
					date = messageResponse.Date;
				}

				status = MsgType.ClientOk;
			}

			OkReceive?.Invoke(this, (status, date));
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
