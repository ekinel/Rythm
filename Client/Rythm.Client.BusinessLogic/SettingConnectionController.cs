using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
//лишние юзинги
namespace Rythm.Client.BusinessLogic
{
    using Rythm.Common.Network;

    public class SettingConnectionController : ISettingConnectionController
    {
        private ITransport _currentTransport;// readonly
        public event Action<string> MessageRecievedEvent;//опечатка

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

        //private void MessageReceived(MessageReceivedEventArgs e)//метод не нужен
        //{
        //    MessageRecievedEvent.Invoke(e.Message);
        //   // ChatMessages += ("\n" + e.Message);
        //}

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
            MessageRecievedEvent.Invoke(e.Message);
        }

        private void HandleConnectionStateChanged(object sender, ConnectionStateChangedEventArgs e)
        {
            ConnectionStateChanged(e);
        }
    }
}
