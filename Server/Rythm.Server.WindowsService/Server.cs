// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.WindowsService
{
    using Service;

    public class Server
    {
        #region Fields

        public NetworkManager _networkManager;

        #endregion

        #region Methods

        public void Start()
        {
            _networkManager = new NetworkManager();
            _networkManager.Start();
        }

        public void Stop()
        {
            _networkManager.Stop();
        }

        #endregion
    }
}
