namespace Rythm.Client.BusinessLogic
{
    using System;
    using Rythm.Common.Network;

    public class SettingConnectionController : ISettingConnectionController
    {
        private readonly ITransport _currentTransport;
        public event Action<string> MessageReceivedEvent;

        public SettingConnectionController()
        {
            _currentTransport = TransportFactory.Create((TransportType.WebSocket));
            _currentTransport.ConnectionStateChanged += HandleConnectionStateChanged;
            _currentTransport.MessageReceived += HandleMessageReceived;
            _currentTransport.Connect("127.0.0.1", "65000");
        }

        public void MessageSend(string CurrentMessage)
        {
            _currentTransport?.Send("\n" + CurrentMessage);
        }

        private void ConnectionStateChanged(ConnectionStateChangedEventArgs e)//параметр должен быть говорящий, одна буква не пойдёт
        {
            //удаляем все что не используется 
            if (e.Connected)
            {
                if (string.IsNullOrEmpty(e.ClientName))
                {
                    //ChatMessages += ("\n" + ("Клиент подключен к серверу."));
                    //MessageReceived("Клиент подключен к серверу.");
                    //ChatMessages += ("\n" + "Авторизуйтеся, чтобы отправлять сообщения.");
                    _currentTransport?.Login("User");
                }
                else
                {
                    //ChatMessages += ("\n" + ("Авторизация выполнена успешно."));
                }
            }
            else
            {
                //ChatMessages += ("\n" + ("Клиент отключен от сервера."));
            }
        }
        private void HandleMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            MessageReceivedEvent.Invoke(e.Message);
        }

        private void HandleConnectionStateChanged(object sender, ConnectionStateChangedEventArgs e)
        {
            ConnectionStateChanged(e);
        }
    }
}
