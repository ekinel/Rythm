// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic
{
	using System;
	using System.Collections.Generic;

	using Common.Network;
	using Common.Network.Messages;

	using Interfaces;

	using Newtonsoft.Json.Linq;

	public class DisplayingEventsController : IDisplayingEventsController
	{
		#region Events

		public event Action<List<string>> UpdatedDataBaseClientsEvent;
		public event Action<List<string>, List<string>, List<string>, List<string>> UpdatedDataBaseMessagesEvent;
		public event Action<List<string>, List<string>> UpdatedDataBaseEventsEvent;

		#endregion

		#region Constructors

		public DisplayingEventsController(IConnectionController connectionServiceController)
		{
			ITransport currentTransport = connectionServiceController.CurrentTransport ??
			                              throw new ArgumentNullException(nameof(connectionServiceController));
			currentTransport.UpdatedDataBaseClients += HandleUpdatedDataBaseClients;
			currentTransport.UpdatedDataBaseMessages += HandleUpdatedDataBaseMessages;
			currentTransport.UpdatedDataBaseEvents += HandleUpdatedDataBaseEvents;
		}

		#endregion

		#region Methods

		private void HandleUpdatedDataBaseClients(object sender, MessageContainer msgContainer)
		{
			if (((JObject)msgContainer.Payload).ToObject(typeof(UpdatedDataBaseClients)) is UpdatedDataBaseClients messageRequest)
			{
				UpdatedDataBaseClientsEvent?.Invoke(messageRequest.ClientsList);
			}
		}

		private void HandleUpdatedDataBaseMessages(object sender, MessageContainer msgContainer)
		{
			if (((JObject)msgContainer.Payload).ToObject(typeof(UpdatedDataBaseMessages)) is UpdatedDataBaseMessages messageRequest)
			{
				UpdatedDataBaseMessagesEvent?.Invoke(
					messageRequest._msgFrom,
					messageRequest._msgTo,
					messageRequest._msgText,
					messageRequest._msgDate);
			}
		}

		private void HandleUpdatedDataBaseEvents(object sender, MessageContainer msgContainer)
		{
			if (((JObject)msgContainer.Payload).ToObject(typeof(UpdatedDataBaseEvents)) is UpdatedDataBaseEvents messageRequest)
			{
				UpdatedDataBaseEventsEvent?.Invoke(messageRequest.EventsList, messageRequest.DateList);
			}
		}

		#endregion
	}
}
