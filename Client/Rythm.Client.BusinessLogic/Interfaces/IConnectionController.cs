// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic.Interfaces
{
	using System;

	using Common.Network;

	public interface IConnectionController
	{
		#region Properties

		ITransport CurrentTransport { get; }

		string Login { get; set; }

		#endregion

		#region Events

		event Action<bool> ConnectionStateChanged;

		#endregion

		#region Methods

		void DoConnect(string address, string port, string login);

		void DoDisconnect();

		#endregion
	}
}
