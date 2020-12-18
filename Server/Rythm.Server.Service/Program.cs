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
				ServerParameters serverParameters = serverConfiguration.ReadConfigurationFile();

				var networkWrapper = new NetworkWrapper(serverParameters.Address, serverParameters.Port, serverParameters.TimeOut, serverParameters.DataBaseConnectionString);
				networkWrapper.Start();


				Console.ReadLine();

				networkWrapper.Stop();
				serverConfiguration.SaveConfigurationFile(serverParameters.Address, serverParameters.Port, serverParameters.TimeOut, serverParameters.DataBaseConnectionString);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				Console.ReadLine();
			}
		}

		#endregion
	}
}
