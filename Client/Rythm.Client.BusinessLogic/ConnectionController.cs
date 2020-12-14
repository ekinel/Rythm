// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic
{
	using System;

	using Common.Network;
	using Common.Network.Enums;
	using Common.Network.Messages;

	using Interfaces;

	public class ConnectionController : IConnectionController
	{
		#region Properties

		public ITransport CurrentTransport { get; }

		public string Login { get; set; }

		#endregion

		#region Events

		public event Action<bool> ConnectionStateChanged;

		#endregion

		#region Constructors

		public ConnectionController()
		{
			CurrentTransport = TransportFactory.Create(TransportType.WebSocket);
			CurrentTransport.ConnectionStateChanged += HandleConnectionStateChanged;
			CurrentTransport.ConnectionOpened += HandleConnectionOpened;
		}

		#endregion

		#region Methods

		public void DoConnect(string address, string port, string login)
		{
			CurrentTransport.Connect(address, port);
			Login = login;
		}

		public void DoDisconnect()
		{
			CurrentTransport.Disconnect();
		}

		private void HandleConnectionStateChanged(object sender, ConnectionStateChangedEventArgs state)
		{
			ConnectionStateChanged?.Invoke(state.Connected);
		}

		private void HandleConnectionOpened(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(Login))
			{
				var msgContainer = new ConnectionRequest(Login);
				CurrentTransport?.Send(msgContainer);
			}
		}

		#endregion
	}
}
