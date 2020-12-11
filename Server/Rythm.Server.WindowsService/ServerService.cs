// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.WindowsService
{
    using System.ComponentModel;
    using System.ServiceProcess;
    using System.Threading;

    using Service;

    public partial class ServerService : ServiceBase
    {
        #region Fields

        public NetworkManager _networkManager;

        #endregion

        #region Constructors

        public ServerService()
        {
            InitializeComponent();
        }

        #endregion

        #region Methods

        protected override void OnStart(string[] args)
        {
	        _networkManager.Start();
        }

        protected override void OnStop()
        {
	        _networkManager.Stop();
        }

        #endregion
    }
}
