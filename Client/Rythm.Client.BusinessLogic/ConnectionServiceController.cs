namespace Rythm.Client.BusinessLogic
{
    using System;
    using Rythm.Common.Network;
    public class ConnectionServiceController : IConnectionServiceController
    {
        private string _login;
        public ITransport CurrentTransport { get;} 
        public ConnectionServiceController()
        {
            CurrentTransport = TransportFactory.Create((TransportType.WebSocket));
        }

       public void DataSending(string address, string port, string login)
        {
            CurrentTransport.Connect(address, port);
            _login = login;
        }
       public string Login { get => _login; set { _login = value; } } 
    }
}
