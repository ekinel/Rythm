// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network
{
    public class ConnectionStateChangedEventArgs
    {
        #region Properties

        public bool Connected { get; }

        #endregion

        #region Constructors

        public ConnectionStateChangedEventArgs(bool connected)
        {
            Connected = connected;
        }

        #endregion
    }
}
