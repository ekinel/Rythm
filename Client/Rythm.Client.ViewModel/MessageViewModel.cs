using System.Windows.Input;

using Prism.Mvvm;
using Prism.Commands;

namespace Rythm.Client.ViewModel
{
    public class MessageViewModel : BindableBase
    {
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
        }
        private void ExecuteCommand()
        {
            ChatMessages = CurrentMessage;
            ChatMessages = "";
        }
    }
}