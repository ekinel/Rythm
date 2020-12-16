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
	using System.Timers;

	using Enums;

	using Messages;

	using Newtonsoft.Json.Linq;

	using Server.Dal;

	using WebSocketSharp.Server;

	public class WsServer
	{
		private const int TIMER_TIME_SECOND = 10000;
		#region Fields

		private readonly IPEndPoint _listenAddress;
		private readonly ConcurrentDictionary<string, WsConnection> _connections;
		private readonly ConcurrentDictionary<string, ClientActivity> _clientsActivity;

		private WebSocketServer _server;
		private bool _isCommonChatCreated = true;

		private readonly int _timeOut;

		private List<string> _clientsNotActiveList;
		private readonly Timer _serverTimer;

		private readonly IRepository<NewClientDataBase> ClientDataBase;
		private readonly IRepository<NewMessageDataBase> MsgDataBase;
		private readonly IRepository<NewEventDataBase> EventDataBase;

		#endregion

		#region Constructors

		public WsServer(IPEndPoint listenAddress, int timeOut, IRepository<NewClientDataBase> clientDataBase, IRepository<NewMessageDataBase> msgDataBase, IRepository<NewEventDataBase> eventDataBase)
		{
			_listenAddress = listenAddress;
			_timeOut = timeOut;
			_connections = new ConcurrentDictionary<string, WsConnection>();
			_clientsActivity = new ConcurrentDictionary<string, ClientActivity>();

			ClientDataBase = clientDataBase;
			MsgDataBase = msgDataBase;
			EventDataBase = eventDataBase;

			_clientsNotActiveList = new List<string>(GetDataBaseClientsListToString());

			_serverTimer = new Timer(TIMER_TIME_SECOND);
			_serverTimer.Elapsed += HandleOnTimedEvent;
			_serverTimer.Enabled = true;
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
			EventDataBase.Create(new NewEventDataBase(){Date = DateTime.Now.ToString(), Message = "Server started"});
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

			_clientsNotActiveList = new List<string>(GetDataBaseClientsListToString());
			EventDataBase.Create(new NewEventDataBase() { Date = DateTime.Now.ToString(), Message = "Server stopped" });
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
							_clientsNotActiveList.Remove(connection.Login);

							SendUpdatedClientsList(new UpdatedClientsResponse(_connections.Keys, _clientsNotActiveList));
							_clientsActivity.TryAdd(connection.Login, new ClientActivity(connection.Login));

							EventDataBase.Create(new NewEventDataBase() { Date = DateTime.Now.ToString(), Message = $"Client {connection.Login} connected" });

							if (_isCommonChatCreated)
							{
								_connections.TryAdd(Properties.Resources.CommonChat, connection);
								_isCommonChatCreated = !_isCommonChatCreated;

								SendUpdatedClientsList(new UpdatedClientsResponse(_connections.Keys, _clientsNotActiveList));
							}

							var dataBaseListLoginsString = GetDataBaseClientsListToString();

							if (!dataBaseListLoginsString.Contains(connection.Login))
							{
								ClientDataBase.Create(new NewClientDataBase() { Login = connection.Login });
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

						if (textMsgContainer.To == Properties.Resources.CommonChat)
						{
							BroadcastSend(container, textMsgContainer.From);
						}
						else
						{
							SendMessage(container, textMsgContainer.To);
							SendMessage(serverOkContainer, textMsgContainer.From);
						}

						UpdateLastClientActivity(textMsgContainer.From);

						MsgDataBase.Create(new NewMessageDataBase(){Date = textMsgContainer.Date, Message = textMsgContainer.Message, ClientFrom = textMsgContainer.From, ClientTo = textMsgContainer.To});
					}

					break;

				case MsgType.ClientOk:

					if (((JObject)container.Payload).ToObject(typeof(ClientOkMsgResponse)) is ClientOkMsgResponse clientOkMsgContainer)
					{
						MessageContainer clientOkContainer = clientOkMsgContainer.GetContainer();

						SendMessage(clientOkContainer, clientOkMsgContainer.From);
						UpdateLastClientActivity(clientOkMsgContainer.From);
					}

					break;
			}
		}

		internal void FreeConnection(string login)
		{
			if (login != Properties.Resources.CommonChat && _connections.TryGetValue(login, out WsConnection x))
			{
				_connections[login].Close();
				_connections.TryRemove(login, out WsConnection connection);
				_clientsNotActiveList.Add(login);

				SendUpdatedClientsList(new UpdatedClientsResponse(_connections.Keys, _clientsNotActiveList));

				EventDataBase.Create(new NewEventDataBase() { Date = DateTime.Now.ToString(), Message = $"Client {connection.Login} disconnected" });
			}
		}

		private void HandleOnTimedEvent(object source, ElapsedEventArgs e)
		{
			int serverTimeSeconds = DateTime.Now.Second;
			int serverTimeMinute = DateTime.Now.Minute;

			foreach (KeyValuePair<string, ClientActivity> client in _clientsActivity)
			{
				DateTime clientTime = DateTime.Parse(client.Value.LastActivityTime);

				int clientTimeSeconds = clientTime.Second;
				int clientTimeMinute = clientTime.Minute;

				int deltaTimeSeconds = serverTimeSeconds - clientTimeSeconds;
				int deltaTimeMinute = serverTimeMinute - clientTimeMinute;

				if (deltaTimeSeconds < 0)
				{
					deltaTimeSeconds = 60 - clientTimeSeconds + serverTimeSeconds;
				}

				if (deltaTimeMinute > 0)
				{
					deltaTimeSeconds += deltaTimeMinute * 60;
				}

				if (deltaTimeSeconds > _timeOut)
				{
					FreeConnection(client.Key);
					_clientsActivity.TryRemove(client.Key, out ClientActivity value);
				}
			}
		}

		private void UpdateLastClientActivity(string login)
		{
			_clientsActivity.TryGetValue(login, out ClientActivity lastTime);
			_clientsActivity.TryUpdate(login, new ClientActivity(login), lastTime);
		}

		private void BroadcastSend(MessageContainer msgContainer, string loginFrom)
		{
			var messageRequest = ((JObject)msgContainer.Payload).ToObject(typeof(MessageRequest)) as MessageRequest;
			var textMsgRequest = ((JObject)messageRequest.MsgContainer).ToObject(typeof(TextMsgRequest)) as TextMsgRequest;

			foreach (KeyValuePair<string, WsConnection> connection in _connections)
			{
				if (connection.Value.Login != loginFrom && connection.Key != Properties.Resources.CommonChat)
				{
					connection.Value.Send(
						new CommonChatMsgResponse(loginFrom, Properties.Resources.CommonChat, textMsgRequest.Message, textMsgRequest.Date).GetContainer());
				}
			}
		}

		private void SendMessage(MessageContainer msgContainer, string targetId)
		{
			foreach (KeyValuePair<string, WsConnection> connection in _connections)
			{
				if (connection.Key == targetId && connection.Key != Properties.Resources.CommonChat)
				{
					connection.Value.Send(msgContainer);
				}
			}
		}

		private void SendUpdatedClientsList(UpdatedClientsResponse updatedClientsResponse)
		{
			foreach (KeyValuePair<string, WsConnection> connection in _connections)
			{
				if (connection.Key != Properties.Resources.CommonChat)
				{
					ICollection<string> updatedActiveClientsList = new List<string>(updatedClientsResponse.ActiveUsersList);
					updatedActiveClientsList.Remove(connection.Key);
					var newUpdatedClientsResponse = new UpdatedClientsResponse(updatedActiveClientsList, updatedClientsResponse.NotActiveUsersList);
					connection.Value.Send(newUpdatedClientsResponse.GetContainer());
				}
			}
		}

		private List<string> GetDataBaseClientsListToString()
		{
			var dataBaseClientsList = ClientDataBase.GetList();
			var clientsList = new List<string>();

			foreach (NewClientDataBase element in dataBaseClientsList)
			{
				clientsList.Add(element.Login);
			}

			return clientsList;
		}

		private List<string> GetActiveClientsList(string login)
		{
			var clientsList = new List<string>();

			foreach (KeyValuePair<string, WsConnection> connection in _connections)
			{
				if (connection.Key != login) 
				clientsList.Add(connection.Key);
			}

			return clientsList;
		}

		private List<string> GetNotActiveClientsList(string login)
		{
			List<string> clientsList = GetDataBaseClientsListToString();
			clientsList.Remove(login);

			foreach (KeyValuePair<string, WsConnection> connection in _connections)
			{
				clientsList.Remove(connection.Key);
			}

			return clientsList;
		}

		#endregion
	}
}
