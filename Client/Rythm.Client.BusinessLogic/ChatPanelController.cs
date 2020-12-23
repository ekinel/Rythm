﻿// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic
{
	using System;
	using System.Collections.Generic;

	using Common.Network;
	using Common.Network.Enums;
	using Common.Network.Messages;

	using Interfaces;

	using Newtonsoft.Json.Linq;

	public class ChatPanelController : IChatPanelController
	{
		#region Fields

		private readonly IConnectionController _connectionServiceController;
		private readonly ITransport _currentTransport;

		#endregion

		#region Events

		public event Action<MessageReceivedEventArgs> MessageReceivedEvent;
		public event Action<(MsgType, DateTime)> OkReceivedEvent;
		public event Action<List<string>, List<string>> UpdatedUsersListEvent;

		#endregion

		#region Constructors

		public ChatPanelController(IConnectionController connectionServiceController)
		{
			_currentTransport = connectionServiceController.CurrentTransport ?? throw new ArgumentNullException(nameof(connectionServiceController));
			_connectionServiceController = connectionServiceController;
			_currentTransport.MessageReceived += HandleMessageReceived;
			_currentTransport.UpdatedUsersList += HandleUpdatedUsersList;
			_currentTransport.OkReceive += HandleOkReceived;
		}

		#endregion

		#region Methods

		public void MessageSend(string currentMessage, string loginTo)
		{
			var msgContainer = new TextMsgRequest(_connectionServiceController.Login, loginTo, currentMessage);
			var mr = new MessageRequest(msgContainer, MsgType.PersonalMessage);
			_currentTransport?.Send(mr);
		}

		private void HandleMessageReceived(object sender, MessageReceivedEventArgs state)
		{
			ApplicationDispatcherHelper.BeginInvoke(() => MessageReceivedEvent?.Invoke(state));
		}

		private void HandleUpdatedUsersList(object sender, MessageContainer msgContainer)
		{
			if (((JObject)msgContainer.Payload).ToObject(typeof(UpdatedClientsResponse)) is UpdatedClientsResponse messageRequest)
			{
				UpdatedUsersListEvent?.Invoke(messageRequest.ActiveUsersList, messageRequest.NotActiveUsersList);
			}
		}

		private void HandleOkReceived(object sender, (MsgType, DateTime) okReceive)
		{
			ApplicationDispatcherHelper.Invoke(() => OkReceivedEvent?.Invoke(okReceive));
		}

		#endregion
	}
}
