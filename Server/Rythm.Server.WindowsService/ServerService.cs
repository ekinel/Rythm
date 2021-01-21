// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.WindowsService
{
	using System.ServiceProcess;
	using System.Threading.Tasks;
	using Service;

	public partial class ServerService : ServiceBase
	{
		#region Fields

		private NetworkWrapper _networkWrapper; 
		private ServerParameters _serverParameters;

		#endregion

		#region Constructors

		public ServerService()
		{
			InitializeComponent();

			ServerConfiguration _serverConfiguration = new ServerConfiguration();
			_serverParameters = _serverConfiguration.ReadConfigurationFile();
		}

		#endregion

		#region Methods

		protected async override void OnStart(string[] args)
		{
			await Task.Run(() => InitializationServer(_serverParameters.Address, _serverParameters.Port, _serverParameters.TimeOut, _serverParameters.DataBaseConnectionString));
		}

		protected override void OnStop()
		{
			_networkWrapper.Stop();
		}

		private void InitializationServer(string wsAddress, int wsPort, int wsTimeOut, string wsDataBase)
		{
			_networkWrapper = new NetworkWrapper(wsAddress, wsPort, wsTimeOut, wsDataBase);
			_networkWrapper.Start();
		}

		#endregion
	}
}
