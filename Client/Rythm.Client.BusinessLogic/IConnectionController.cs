// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic
{
    using Common.Network;

    public interface IConnectionController
    {
        #region Properties

        ITransport CurrentTransport { get; }

        string Login { get; set; }

        #endregion

        #region Methods

        void DataSending(string address, string port, string login);

        #endregion
    }
}
