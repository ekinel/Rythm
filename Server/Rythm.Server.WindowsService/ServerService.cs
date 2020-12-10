// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.WindowsService
{
    using System.ComponentModel;
    using System.ServiceProcess;
    using System.Threading;

    public partial class ServerService : ServiceBase
    {
        #region Fields

        private Server server;

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
            server = new Server();
            server.Start();
            var serverThread = new Thread(server.Start);
            serverThread.Start();
        }

        protected override void OnStop()
        {
            server.Stop();
            Thread.Sleep(1000);
        }

        #endregion
    }
}
