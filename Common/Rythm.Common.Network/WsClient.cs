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

		private bool IsConnected => _socket?.ReadyState == WebSocketState.Open;

		#endregion

		#region Events

		public event EventHandler<ConnectionStateChangedEventArgs> ConnectionStateChanged;
		public event EventHandler<MessageReceivedEventArgs> MessageReceived;
		public event EventHandler<EventArgs> ConnectionOpened;

		public event EventHandler<MessageContainer> UpdatedUsersList;
		public event EventHandler<MessageContainer> UpdatedDataBaseData;
		public event EventHandler<(MsgStatus, DateTime)> OkReceive;

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
					ClientRegistration(container);
					break;

				case MsgType.PersonalMessage:
					PersonalMessage(container);
					break;

				case MsgType.CommonMessage:
					CommonMessage(container);
					break;

				case MsgType.ClientOk:
				case MsgType.ServerOk:
					OkResponse(container);
					break;

				case MsgType.UpdatedClientsList:
					UpdatedUsersList?.Invoke(this, container);
					break;

				case MsgType.UpdatedDataBaseClients:
				case MsgType.UpdatedDataBaseMessages:
				case MsgType.UpdatedDataBaseEvents:
					UpdatedDataBaseData?.Invoke(this, container);
					break;
			}
		}

		private void ClientRegistration(MessageContainer container)
		{
			if (((JObject)container.Payload).ToObject(typeof(ConnectionResponse)) is ConnectionResponse connectionResponse &&
			    connectionResponse.Result == ResultCodes.Failure)
			{
				return;
			}

			ConnectionStateChanged?.Invoke(this, new ConnectionStateChangedEventArgs(true));
		}

		private void PersonalMessage(MessageContainer container)
		{
			if (!(((JObject)container.Payload).ToObject(typeof(MessageRequest)) is MessageRequest messageRequest))
			{
				return;
			}


			if(((JObject)messageRequest.MsgContainer).ToObject(typeof(TextMsgRequest)) is TextMsgRequest textMsgRequest)
			{
				MessageReceived?.Invoke(this, new MessageReceivedEventArgs(textMsgRequest));
				var msgContainer = new ClientOkMsgResponse(textMsgRequest.From, textMsgRequest.To, textMsgRequest.Date);
				Send(msgContainer);
			}
		}

		private void CommonMessage(MessageContainer container)
		{
			if(((JObject)container.Payload).ToObject(typeof(CommonChatMsgResponse)) is CommonChatMsgResponse msgRequest)
			{
				var mess = new MessageReceivedEventArgs(new TextMsgRequest(msgRequest.From, "CommonChat", msgRequest.Message, MsgStatus.None));
				MessageReceived?.Invoke(this, mess);
				var mgContainer = new ClientOkMsgResponse(msgRequest.From, "CommonChat", msgRequest.Date);
				Send(mgContainer);
			}
		}

		private void OkResponse(MessageContainer container)
		{
			MsgType type = container.Identifier;
			MsgStatus status;
			DateTime date = DateTime.Now;

			if (type == MsgType.ServerOk)
			{
				if (((JObject)container.Payload).ToObject(typeof(ServerOkMsgResponse)) is ServerOkMsgResponse messageResponse)
				{
					date = messageResponse.Date;
				}

				status = MsgStatus.ServerOk;
			}
			else
			{
				if (((JObject)container.Payload).ToObject(typeof(ClientOkMsgResponse)) is ClientOkMsgResponse messageResponse)
				{
					date = messageResponse.Date;
				}

				status = MsgStatus.ClientOk;
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
