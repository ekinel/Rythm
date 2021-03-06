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
	using Rythm.Common.Network.Interfaces;

	public class DisplayingEventsController : IDisplayingEventsController
	{
		#region Properties

		public string Login { get; set; }

		#endregion

		#region Events

		public event Action<List<string>> UpdatedDataBaseClientsEvent;
		public event Action<List<DataBaseMessage>> UpdatedDataBaseMessagesEvent;
		public event Action<List<DataBaseEvent>> UpdatedDataBaseEventsEvent;

		#endregion

		#region Constructors

		public DisplayingEventsController(IConnectionController connectionServiceController)
		{
			ITransport currentTransport = connectionServiceController.CurrentTransport ??
			                              throw new ArgumentNullException(nameof(connectionServiceController));
			currentTransport.UpdatedDataBaseData += HandleUpdatedDataBaseClients;
		}

		#endregion

		#region Methods

		private void HandleUpdatedDataBaseClients(object sender, MessageContainer msgContainer)
		{
			switch (msgContainer.Identifier)
			{
				case MsgType.UpdatedDataBaseClients:
					if (((JObject)msgContainer.Payload).ToObject(typeof(UpdatedDataBaseClients)) is UpdatedDataBaseClients messageRequest)
					{
						ApplicationDispatcherHelper.BeginInvoke(() => UpdatedDataBaseClientsEvent?.Invoke(messageRequest.ClientsList));
					}

					break;

				case MsgType.UpdatedDataBaseMessages:
					if (((JObject)msgContainer.Payload).ToObject(typeof(UpdatedDataBaseMessages)) is UpdatedDataBaseMessages messageRequest1)
					{
						var personalMessagesList = new List<DataBaseMessage>();
						foreach (DataBaseMessage message in messageRequest1.MessagesList)
						{
							if (message.ClientFrom == Login || message.ClientTo == Login || message.ClientTo == "CommonChat")
							{
								personalMessagesList.Add(message);
							}
						}

						ApplicationDispatcherHelper.BeginInvoke(() => UpdatedDataBaseMessagesEvent?.Invoke(personalMessagesList));
					}

					break;

				case MsgType.UpdatedDataBaseEvents:
					if (((JObject)msgContainer.Payload).ToObject(typeof(UpdatedDataBaseEvents)) is UpdatedDataBaseEvents messageRequest2)
					{
						ApplicationDispatcherHelper.BeginInvoke(() => UpdatedDataBaseEventsEvent?.Invoke(messageRequest2.EventsList));
					}

					break;
			}
		}

		#endregion
	}
}
