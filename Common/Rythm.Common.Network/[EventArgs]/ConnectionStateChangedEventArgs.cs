namespace Rythm.Common.Network
{
    public class ConnectionStateChangedEventArgs
    {
        #region Properties

        public string ClientName { get; }

        public bool Connected { get; }

        #endregion Properties

        #region Constructors

        public ConnectionStateChangedEventArgs(string clientName, bool connected)
        {
            ClientName = clientName;
            Connected = connected;
        }

        #endregion Constructors
    }
}
