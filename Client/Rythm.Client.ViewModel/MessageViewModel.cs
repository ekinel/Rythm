using System.Windows;
using System.Windows.Threading;

namespace Rythm.Client.ViewModel
{
    using System;
    using System.Windows.Input;

    using System.Collections.ObjectModel;

    using Prism.Mvvm;
    using Prism.Commands;

    using Rythm.Common.Network;
    using Rythm.Client.BusinessLogic;
    public class MessageViewModel : BindableBase
    {
        private readonly IMessagingServiceController _settingConnectionController;

        private string _outgoingMessage;
        private string _chatMessages;
        public ICommand SendCommand { get; }

        public ObservableCollection<SendMessage> ReceivedMessagesList { get; set; } = new ObservableCollection<SendMessage>();

        public string OutgoingMessage
        {
            get => _outgoingMessage;
            set => SetProperty(ref _outgoingMessage, value);
        }
        public string ChatMessages
        {
            get => _chatMessages;
            set => SetProperty(ref _chatMessages, value);
        }

        public void HandleNewMessageRecieved(string message)
        {
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(() => {
                    ReceivedMessagesList.Add(new SendMessage("User1", message ));
                }));
        }

        public MessageViewModel(IMessagingServiceController settingConnectionController)
        {
            SendCommand = new DelegateCommand(SendMessageCommand);
            _settingConnectionController = settingConnectionController ?? throw new ArgumentNullException(nameof(settingConnectionController));
            _settingConnectionController.MessageReceivedEvent += HandleNewMessageRecieved;

        }
        private void SendMessageCommand()
        {
            _settingConnectionController.MessageSend(OutgoingMessage);
            OutgoingMessage = string.Empty;
        }
    }
}