using System.Windows.Input;

using Prism.Mvvm;
using Prism.Commands;

using Rythm.Common.Network;
using Rythm.Client.BusinessLogic;

namespace Rythm.Client.ViewModel
{
    public class MessageViewModel : BindableBase
    {
        public ISettingConnectionController _settingConnectionController;

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
        public void NewMessageRecieved(string Message)
        {
            ChatMessages = Message;
        }
        public MessageViewModel(ISettingConnectionController settingConnectionController)
        {
            SendCommand = new DelegateCommand(ExecuteCommand);
            _settingConnectionController = settingConnectionController;
            _settingConnectionController.MessageRecievedEvent += NewMessageRecieved;
        }
        private void ExecuteCommand()
        {
            //_currentTransport?.Send("\n" + CurrentMessage);
            _settingConnectionController.MessageSend("\n" + CurrentMessage);
            CurrentMessage = "";
        }

        public void MessageReceived(MessageReceivedEventArgs e)
        {
            ChatMessages += ("\n" + e.Message);
        }
    }
}