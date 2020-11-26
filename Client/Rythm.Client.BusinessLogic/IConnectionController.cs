// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic
{
    using System;

    using Common.Network;

    public interface IConnectionController
    {
        #region Properties

        ITransport CurrentTransport { get; }
        IUserLoginDisplayController UserLoginDisplayController { get; set; }

        string Login { get; set; }
        string ConnectionParametersViewVisibility { get; set; }

        #endregion

        #region Events

        event Action<string> SendNewStateParametersViewVisibility;

        #endregion

        #region Methods

        void DataSending(string address, string port, string login);

        #endregion
    }
}
