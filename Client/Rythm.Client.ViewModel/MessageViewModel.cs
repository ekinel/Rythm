using System.Windows.Input;

using Prism.Mvvm;
using Prism.Commands;

using Rythm.Common.Network;

namespace Rythm.Client.ViewModel
{
    public class MessageViewModel : BindableBase
    {
        private ITransport _currentTransport;

        private string _currentMessage;
        private string _chatMessages;
        public ICommand SendCommand { get; }

        public string CurrentMessage
        {
            get => _currentMessage;
            set => SetProperty(ref _currentMessage, value);
        }
        public string ChatMessages
        {
            get => _chatMessages;
            set => SetProperty(ref _chatMessages, value);
        }
        public MessageViewModel()
        {
            SendCommand = new DelegateCommand(ExecuteCommand);


            _currentTransport = TransportFactory.Create((TransportType.WebSocket));
            _currentTransport.ConnectionStateChanged += HandleConnectionStateChanged;
            _currentTransport.MessageReceived += HandleMessageReceived;
            _currentTransport.Connect("127.0.0.1", "65000");
            _currentTransport?.Login("User");
        }
        private void ExecuteCommand()
        {
            //ChatMessages += ("\n" + CurrentMessage);
            _currentTransport?.Send("\n" + CurrentMessage);

            CurrentMessage = "";
        }

        private void HandleMessageReceived(MessageReceivedEventArgs e)
        {
            ChatMessages += ("\n" + e.Message);
        }

        private void HandleConnectionStateChanged(ConnectionStateChangedEventArgs e)
        {
            if (e.Connected)
            {
                if (string.IsNullOrEmpty(e.ClientName))
                {
                    ChatMessages += ("\n" + ("Клиент подключен к серверу."));
                    ChatMessages += ("\n" + "Авторизуйтеся, чтобы отправлять сообщения.");
                }
                else
                {
                    ChatMessages += ("\n" + ("Авторизация выполнена успешно."));
                }
            }
            else
            {
                ChatMessages += ("\n" + ("Клиент отключен от сервера."));
            }
        }

        private void HandleMessageReceived(object sender, MessageReceivedEventArgs e)
        {
                HandleMessageReceived(e);
        }

        private void HandleConnectionStateChanged(object sender, ConnectionStateChangedEventArgs e)
        {
                HandleConnectionStateChanged(e);

        }
    }
}