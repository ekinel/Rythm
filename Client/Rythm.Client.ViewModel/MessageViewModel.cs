// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;

    using BusinessLogic;

    using Prism.Commands;
    using Prism.Mvvm;

    public class MessageViewModel : BindableBase
    {
        #region Fields

        private readonly IMessagingController _settingConnectionController;

        private string _outgoingMessage;
        private string _chatMessages;
        private string _login;

        #endregion

        #region Properties

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

        #endregion

        #region Constructors

        public MessageViewModel(IMessagingController settingConnectionController)
        {
            SendCommand = new DelegateCommand(SendMessageCommand);
            _settingConnectionController = settingConnectionController ?? throw new ArgumentNullException(nameof(settingConnectionController));
            _settingConnectionController.MessageReceivedEvent += HandleNewMessageRecieved;
            _settingConnectionController.UserLoginEvent += HandleUserLogin;
        }

        #endregion

        #region Methods

        public void HandleNewMessageRecieved(string message)
        {
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(
                    () =>
                    {
                        ReceivedMessagesList.Add(new SendMessage(_login, message));
                    }));
        }

        public void HandleUserLogin(string login)
        {
            _login = login;
        }

        private void SendMessageCommand()
        {
            _settingConnectionController.MessageSend(OutgoingMessage);
            OutgoingMessage = string.Empty;
        }

        #endregion
    }
}
