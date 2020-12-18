// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Service
{
	using System;
	using System.IO;

	using Newtonsoft.Json;

	public class ServerConfiguration
	{
		#region Methods

		public void SaveConfigurationFile(string address, int port, int timeOut, string dataBaseConnectionString)
		{
			var serverParameters = new ServerParameters(address, port, timeOut, dataBaseConnectionString);
			string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "Configuration.json";

			using (StreamWriter file = File.CreateText(path))
			{
				var serializer = new JsonSerializer();
				serializer.Serialize(file, serverParameters);
			}
		}

		public ServerParameters ReadConfigurationFile()
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "Configuration.json";
			if (!File.Exists(path))
			{
				return new ServerParameters(string.Empty, 0, 0, string.Empty);
			}

			string configurationString = File.ReadAllText(path);

			var container = JsonConvert.DeserializeObject<ServerParameters>(configurationString);
			return container;
		}

		#endregion
	}
}
