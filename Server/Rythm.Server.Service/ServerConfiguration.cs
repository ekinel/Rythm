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
		#region Fields

		private readonly string _path;

		#endregion

		#region Constructors

		public ServerConfiguration()
		{
			_path = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\\Rythm\\Configuration.json";
		}

		#endregion

		#region Methods

		public void SaveConfigurationFile(string address, int port, int timeOut, string dataBaseConnectionString)
		{
			var serverParameters = new ServerParameters(address, port, timeOut, dataBaseConnectionString);

			if (!File.Exists(_path))
			{
				return;
			}

			using (StreamWriter file = File.CreateText(_path))
			{
				var serializer = new JsonSerializer();
				serializer.Serialize(file, serverParameters);
			}
		}

		public ServerParameters ReadConfigurationFile()
		{
			if (!File.Exists(_path))
			{
				return new ServerParameters(string.Empty, 0, 0, string.Empty);
			}

			string configurationString = File.ReadAllText(_path);

			var container = JsonConvert.DeserializeObject<ServerParameters>(configurationString);
			return container;
		}

		#endregion
	}
}
