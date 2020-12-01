namespace Rythm.Common.Network
{
    public class ConnectionStateChangedEventArgs
    {
        #region Properties

        public bool Connected { get; }

        #endregion Properties

        #region Constructors

        public ConnectionStateChangedEventArgs(bool connected)
        {
            Connected = connected;
        }

        #endregion Constructors
    }
}
