// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Service
{
	using System;

	internal class Server
	{
		#region Methods

		private static void Main(string[] args)
		{
			try
			{
				var serverConfiguration = new ServerConfiguration();
				NetworkWrapper networkWrapper;

				if (FillingFields(serverConfiguration))
				{
					ServerParameters serverParameters = serverConfiguration.ReadConfigurationFile();
					networkWrapper = new NetworkWrapper(serverParameters.Address, serverParameters.Port, serverParameters.TimeOut, serverParameters.DataBaseConnectionString);

				}
				else
				{
					networkWrapper = new NetworkWrapper();
				}

				networkWrapper.Start();

				Console.ReadLine();

				networkWrapper.Stop();
				//serverConfiguration.SaveConfigurationFile(serverParameters.Address, serverParameters.Port, serverParameters.TimeOut, serverParameters.DataBaseConnectionString);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				Console.ReadLine();
			}
		}

		#endregion

		private static bool FillingFields(ServerConfiguration serverConfiguration)
		{
			ServerParameters serverParameters = serverConfiguration.ReadConfigurationFile();
			return !string.IsNullOrEmpty(serverParameters.Address);
		}
	}
}
