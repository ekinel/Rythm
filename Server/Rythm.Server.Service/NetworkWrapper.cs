// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Service
{
	using Dal;

	public class NetworkWrapper
	{
		#region Fields

		private readonly NetworkManager _networkManager;
		private readonly ClientRepository _clientRepository = new ClientRepository();
		private readonly MessageRepository _messageRepository = new MessageRepository();
		private readonly EventRepository _eventRepository = new EventRepository();


		#endregion

		#region Constructors

		public NetworkWrapper()
		{
			_networkManager = new NetworkManager(_clientRepository, _messageRepository, _eventRepository);
		}

		public NetworkWrapper(string address, int port, int timeOut, string dataBaseConnectionString)
		{
			_networkManager = new NetworkManager(address, port, timeOut, dataBaseConnectionString, _clientRepository, _messageRepository, _eventRepository);
		}

		#endregion

		#region Methods

		public void Start()
		{
			_networkManager.Start();
		}

		public void Stop()
		{
			_networkManager.Stop();
		}

		#endregion
	}
}
