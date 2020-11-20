using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Rythm.Client.BusinessLogic
{
    using Rythm.Common.Network;

    public class SettingConnectionController : ISettingConnectionController
    {
        private ITransport _currentTransport;
        public event Action<string> MessageRecievedEvent;

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

        public void MessageReceived(MessageReceivedEventArgs e)
        {
            MessageRecievedEvent.Invoke(e.Message);
           // ChatMessages += ("\n" + e.Message);
        }

        private void ConnectionStateChanged(ConnectionStateChangedEventArgs e)
        {
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
            MessageReceived(e);
        }

        private void HandleConnectionStateChanged(object sender, ConnectionStateChangedEventArgs e)
        {
            ConnectionStateChanged(e);
        }
    }
}
