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

		#endregion

		#region Constructors

		public ServerService()
		{
			InitializeComponent();

			ServerConfiguration _serverConfiguration = new ServerConfiguration();
			ServerParameters _serverParameters = _serverConfiguration.ReadConfigurationFile();


			ServerParameters serverParameters = _serverConfiguration.ReadConfigurationFile();
			_networkWrapper = new NetworkWrapper(
				serverParameters.Address,
				serverParameters.Port,
				serverParameters.TimeOut,
				serverParameters.DataBaseConnectionString);

			_serverConfiguration.SaveConfigurationFile(
				_serverParameters.Address,
				_serverParameters.Port,
				_serverParameters.TimeOut,
				_serverParameters.DataBaseConnectionString);
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
		}

		#endregion
	}
}
