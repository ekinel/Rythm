// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Service
{
	public class ServerParameters
	{
		#region Properties

		public string Address { get; set; }
		public int Port { get; set; }
		public int TimeOut { get; set; }
		public string DataBaseConnectionString { get; set; }

		#endregion

		#region Constructors

		public ServerParameters(string address, int port, int timeOut, string dataBaseConnectionString)
		{
			Address = address;
			Port = port;
			TimeOut = timeOut;
			DataBaseConnectionString = dataBaseConnectionString;
		}

		#endregion
	}
}
