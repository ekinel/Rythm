// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Service
{
	using System;
	using System.IO;
	using System.Text.RegularExpressions;
	using Newtonsoft.Json;

	public class ServerConfiguration
	{
		#region Constants

		private const int DEFAULT_PORT = 65000;
		private const int DEFAULT_TIMEOUT = 20;
		private const string DEFAULT_ADDRESS = "127.0.0.1";
		private const string DEFAULT_DATABASE_CONNECTION_STRING = "data source=(localdb)/MSSQLLocalDB;Initial Catalog=MessageDb;Integrated Security=True;";

		private const string ADDRESS_PATTERN = @"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}$";
		private const int MIN_PORT = 49152;
		private const int MAX_PORT = 65535;

		#endregion

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
				StreamWriter streamWriter = new StreamWriter(_pathToConfigurationFile);
				streamWriter.Close();
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
				return new ServerParameters(DEFAULT_ADDRESS, DEFAULT_PORT, DEFAULT_TIMEOUT, DEFAULT_DATABASE_CONNECTION_STRING);
			}

			string configurationString = File.ReadAllText(_pathToConfigurationFile);

			var container = JsonConvert.DeserializeObject<ServerParameters>(configurationString);

			if(container == null) return new ServerParameters(DEFAULT_ADDRESS, DEFAULT_PORT, DEFAULT_TIMEOUT, DEFAULT_DATABASE_CONNECTION_STRING);

			return CheckingParameters(container);
		}

		private ServerParameters CheckingParameters(ServerParameters checkingContainer)
		{
			var resultContainer = new ServerParameters(checkingContainer.Address, checkingContainer.Port, checkingContainer.TimeOut, checkingContainer.DataBaseConnectionString);

			var regex = new Regex(ADDRESS_PATTERN);
			Match compare = regex.Match(resultContainer.Address);

			if(!compare.Success)
			{
				resultContainer.Address = DEFAULT_ADDRESS;
			}

			int valuePort = Convert.ToInt32(resultContainer.Port);

			if (!(valuePort > MIN_PORT && valuePort < MAX_PORT))
			{
				resultContainer.Port = DEFAULT_PORT;
			}

			if (resultContainer.TimeOut <= 0)
			{
				resultContainer.TimeOut = DEFAULT_TIMEOUT;
			}

			if(string.IsNullOrEmpty(resultContainer.DataBaseConnectionString))
			{
				resultContainer.DataBaseConnectionString = DEFAULT_DATABASE_CONNECTION_STRING;
			}

			return resultContainer;
		}

		#endregion
	}
}
