// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Service
{
	using System;
	using System.Linq.Expressions;

	internal class Server
	{
		#region Fields

		private static ServerConfiguration _serverConfiguration;
		private static ServerParameters _serverParameters;

		private static NetworkWrapper _networkWrapper;

		#endregion

		#region Methods

		private static void Main(string[] args)
		{
			try
			{
				_serverConfiguration = new ServerConfiguration();

				if (FillingFields(_serverConfiguration))
				{
					_serverParameters = _serverConfiguration.ReadConfigurationFile();
					_networkWrapper = new NetworkWrapper(
						_serverParameters.Address,
						_serverParameters.Port,
						_serverParameters.TimeOut,
						_serverParameters.DataBaseConnectionString);
				}
				else
				{
					_networkWrapper = new NetworkWrapper();
				}

				_networkWrapper.Start();

				Console.ReadLine();

				_networkWrapper.Stop();
				_serverConfiguration.SaveConfigurationFile(
					_serverParameters.Address,
					_serverParameters.Port,
					_serverParameters.TimeOut,
					_serverParameters.DataBaseConnectionString);
			}
			catch (Exception exception)
			{
				_networkWrapper.WriteErrorToDataBase(exception);
				Console.WriteLine(exception);
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
