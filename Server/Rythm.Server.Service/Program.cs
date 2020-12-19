// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Service
{
	using System;

	internal class Server
	{
		#region Fields

		private static ServerConfiguration _serverConfiguration;
		private static ServerParameters _serverParameters;

		#endregion

		#region Methods

		private static void Main(string[] args)
		{
			try
			{
				_serverConfiguration = new ServerConfiguration();
				NetworkWrapper networkWrapper;

				if (FillingFields(_serverConfiguration))
				{
					_serverParameters = _serverConfiguration.ReadConfigurationFile();
					networkWrapper = new NetworkWrapper(
						_serverParameters.Address,
						_serverParameters.Port,
						_serverParameters.TimeOut,
						_serverParameters.DataBaseConnectionString);
				}
				else
				{
					networkWrapper = new NetworkWrapper();
				}

				networkWrapper.Start();

				Console.ReadLine();

				networkWrapper.Stop();
				_serverConfiguration.SaveConfigurationFile(
					_serverParameters.Address,
					_serverParameters.Port,
					_serverParameters.TimeOut,
					_serverParameters.DataBaseConnectionString);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				Console.ReadLine();
			}
		}

		private static bool FillingFields(ServerConfiguration serverConfiguration)
		{
			ServerParameters serverParameters = serverConfiguration.ReadConfigurationFile();
			return !string.IsNullOrEmpty(serverParameters.Address);
		}

		#endregion
	}
}
