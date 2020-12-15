// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Service
{
	using Dal;

	public class NetworkWrapper
	{
		private NetworkManager networkManager;
		private readonly ClientRepository _clientRepository = new ClientRepository();
		private readonly MessageRepository _messageRepository = new MessageRepository();
		private readonly EventRepository _eventRepository = new EventRepository();

		public NetworkWrapper()
		{
			networkManager = new NetworkManager(_clientRepository, _messageRepository, _eventRepository);
		}

		public NetworkWrapper(string address, int port, int timeOut)
		{
			networkManager = new NetworkManager(address, port, timeOut, _clientRepository, _messageRepository, _eventRepository);
		}

		public void Start()
		{
			networkManager.Start();
		}
		public void Stop()
		{
			networkManager.Stop();
		}
	}
}
