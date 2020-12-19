// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.WindowsService
{
	using System.ServiceProcess;

	using Service;

	public partial class ServerService : ServiceBase
	{
		#region Fields

		private readonly NetworkWrapper _networkWrapper;
		private readonly ServerConfiguration _serverConfiguration;
		private readonly ServerParameters _serverParameters;

		#endregion

		#region Constructors

		public ServerService()
		{
			InitializeComponent();

			_serverConfiguration = new ServerConfiguration();
			_serverParameters = _serverConfiguration.ReadConfigurationFile();

			if (FillingFields(_serverConfiguration))
			{
				ServerParameters serverParameters = _serverConfiguration.ReadConfigurationFile();
				_networkWrapper = new NetworkWrapper(
					serverParameters.Address,
					serverParameters.Port,
					serverParameters.TimeOut,
					serverParameters.DataBaseConnectionString);
			}
			else
			{
				_networkWrapper = new NetworkWrapper();
			}
		}

		#endregion

		#region Methods

		protected override void OnStart(string[] args)
		{
			_networkWrapper.Start();
		}

		protected override void OnStop()
		{
			_networkWrapper.Stop();
			_serverConfiguration.SaveConfigurationFile(
				_serverParameters.Address,
				_serverParameters.Port,
				_serverParameters.TimeOut,
				_serverParameters.DataBaseConnectionString);
		}

		private static bool FillingFields(ServerConfiguration serverConfiguration)
		{
			ServerParameters serverParameters = serverConfiguration.ReadConfigurationFile();
			return !string.IsNullOrEmpty(serverParameters.Address);
		}

		#endregion
	}
}
