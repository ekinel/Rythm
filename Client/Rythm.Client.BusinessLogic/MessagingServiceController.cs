namespace Rythm.Client.BusinessLogic
{
    using System;
    using Rythm.Common.Network;

    public class MessagingServiceController : IMessagingServiceController
    {
        private readonly ITransport _currentTransport;
        public event Action<string> MessageReceivedEvent;

        private readonly string _login;

        public MessagingServiceController(IConnectionServiceController connectionServiceController)
        {
            _currentTransport = connectionServiceController.CurrentTransport ?? throw new ArgumentNullException(nameof(connectionServiceController));
            _login = connectionServiceController.Login;
            _currentTransport.ConnectionStateChanged += HandleConnectionStateChanged;
            _currentTransport.MessageReceived += HandleMessageReceived;
        }

        public void MessageSend(string CurrentMessage)
        {
            _currentTransport?.Send(CurrentMessage);
        }

        private void ConnectionStateChanged(ConnectionStateChangedEventArgs state)
        {
            if (state.Connected)
            {
                if (string.IsNullOrEmpty(state.ClientName))
                {
                    // _currentTransport?.Login("User");
                    _currentTransport?.Login(_login);
                }
            }
        }
        private void HandleMessageReceived(object sender, MessageReceivedEventArgs state)
        {
            MessageReceivedEvent.Invoke(state.Message);
        }

        private void HandleConnectionStateChanged(object sender, ConnectionStateChangedEventArgs state)
        {
            ConnectionStateChanged(state);
        }
    }
}
