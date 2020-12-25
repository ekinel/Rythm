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

		private readonly string _pathToConfigurationFile;
		private readonly string _pathToFolder;

		#endregion

		#region Constructors

		public ServerConfiguration()
		{
			_pathToConfigurationFile = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\\Rythm\\Configuration.json";
			_pathToFolder = $"{Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)}\\Rythm";

			Directory.CreateDirectory(_pathToFolder);
		}

		#endregion

		#region Methods

		public void SaveConfigurationFile(string address, int port, int timeOut, string dataBaseConnectionString)
		{
			var serverParameters = new ServerParameters(address, port, timeOut, dataBaseConnectionString);

			if (!File.Exists(_pathToConfigurationFile))
			{
				return;
			}

			using (StreamWriter file = File.CreateText(_pathToConfigurationFile))
			{
				var serializer = new JsonSerializer();
				serializer.Serialize(file, serverParameters);
			}
		}

		public ServerParameters ReadConfigurationFile()
		{
			if (!File.Exists(_pathToConfigurationFile))
			{
				return new ServerParameters(string.Empty, 0, 0, string.Empty);
			}

			string configurationString = File.ReadAllText(_pathToConfigurationFile);

			var container = JsonConvert.DeserializeObject<ServerParameters>(configurationString);

			if(container == null) return new ServerParameters(string.Empty, 0, 0, string.Empty);

			return container;
		}

		#endregion
	}
}
