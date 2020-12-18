// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Service
{
	using System;
	using System.Net;

	using Common.Network;

	using Dal;

	using System.Configuration;


	public class NetworkManager
	{
		#region Constants

		private const int WS_PORT = 65000;
		private const int TIME_OUT = 20;

		#endregion

		#region Fields

		private readonly WsServer _wsServer;

		#endregion

		#region Constructors

		public NetworkManager(
			IRepository<NewClientDataBase> clientDataBase,
			IRepository<NewMessageDataBase> msgDataBase,
			IRepository<NewEventDataBase> eventDataBase)
		{
			_wsServer = new WsServer(new IPEndPoint(IPAddress.Any, WS_PORT), TIME_OUT, clientDataBase, msgDataBase, eventDataBase);
		}

		public NetworkManager(
			string address,
			int port,
			int timeOut,
			string dataBaseConnectionString,
			IRepository<NewClientDataBase> clientDataBase,
			IRepository<NewMessageDataBase> msgDataBase,
			IRepository<NewEventDataBase> eventDataBase)
		{
			if (string.IsNullOrEmpty(address))
			{
				_wsServer = new WsServer(new IPEndPoint(IPAddress.Any, port), timeOut, clientDataBase, msgDataBase, eventDataBase);
			}
			else
			{
				int wsPort = Convert.ToInt32(port);
				IPAddress ipAddress = IPAddress.Parse(address);
				byte[] bytes = ipAddress.GetAddressBytes();
				uint wsAddress = BitConverter.ToUInt32(bytes, 0);

				_wsServer = new WsServer(new IPEndPoint(wsAddress, wsPort), Convert.ToInt32(timeOut), clientDataBase, msgDataBase, eventDataBase);

				var dataBaseConnectionSettings = new ConnectionStringSettings("DBConnection", dataBaseConnectionString, "System.Data.SqlClient");
				Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				config.ConnectionStrings.ConnectionStrings.Clear();
				config.ConnectionStrings.ConnectionStrings.Add(dataBaseConnectionSettings);
				config.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection(config.ConnectionStrings.SectionInformation.Name);
			}
		}

		#endregion

		#region Methods

		public void Start()
		{
			Console.WriteLine($"WebSocketServer: {IPAddress.Any}:{WS_PORT}");
			_wsServer.Start();
		}

		public void Stop()
		{
			_wsServer.Stop();
		}

		#endregion
	}
}
