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
		#region Fields

		private readonly ITransport _currentTransport;

		#endregion

		#region Events

		public event Action<List<string>> UpdatedDataBaseClientsEvent;

		#endregion

		#region Constructors

		public DisplayingEventsController(IConnectionController connectionServiceController)
		{
			_currentTransport = connectionServiceController.CurrentTransport ?? throw new ArgumentNullException(nameof(connectionServiceController));
			_currentTransport.UpdatedDataBaseClients += HandleUpdatedDataBaseClients;
		}

		#endregion

		#region Methods

		private void HandleUpdatedDataBaseClients(object sender, MessageContainer msgContainer)
		{
			var messageRequest = ((JObject)msgContainer.Payload).ToObject(typeof(UpdatedDataBaseClients)) as UpdatedDataBaseClients;
			if (messageRequest != null)
			{
				UpdatedDataBaseClientsEvent?.Invoke(messageRequest.ClientsList);
			}
		}

		#endregion
	}
}
