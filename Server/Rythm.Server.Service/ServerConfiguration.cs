// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Service
{
	using System.IO;

	using Newtonsoft.Json;

	public class ServerConfiguration
	{
		#region Methods

		public void SaveConfigurationFile(string address, int port, int timeOut, string dataBaseConnectionString)
		{
			var serverParameters = new ServerParameters(address, port, timeOut, dataBaseConnectionString);

			using (StreamWriter file = File.CreateText(@"..\..\..\..\Server\Rythm.Server.Service\Configuration.json"))
			{
				var serializer = new JsonSerializer();
				serializer.Serialize(file, serverParameters);
			}
		}

		public ServerParameters UseConfigurationFile()
		{
			string configurationString = File.ReadAllText(@"..\..\..\..\Server\Rythm.Server.Service\Configuration.json");
			var container = JsonConvert.DeserializeObject<ServerParameters>(configurationString);
			return container;
		}

		#endregion
	}
}
