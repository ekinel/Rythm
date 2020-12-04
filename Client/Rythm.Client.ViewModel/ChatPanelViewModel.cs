// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Threading;

    using BusinessLogic;

    using Common.Network;
    using Common.Network.Enums;

    using Events;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;

    public class ChatPanelViewModel : BindableBase
    {
        #region Fields

        private readonly IChatPanelController _chatPanelController;

        private string _outgoingMessage;
        private string _chatMessages;
        private string _loginTo;
        private string _loginFrom;

        #endregion

        #region Properties

        public DelegateCommand SendCommand { get; }

        public ObservableCollection<SendMessageViewModel> ReceivedMessagesList { get; set; } = new ObservableCollection<SendMessageViewModel>();

        public string OutgoingMessage
        {
            get => _outgoingMessage;
            set
            {
                SetProperty(ref _outgoingMessage, value);
                SendCommand.RaiseCanExecuteChanged();
            }
        }

        public string ChatMessages
        {
            get => _chatMessages;
            set => SetProperty(ref _chatMessages, value);
        }

        #endregion

        #region Constructors

        public ChatPanelViewModel(IChatPanelController chatPanelController, IEventAggregator eventAggregator)
        {
            SendCommand = new DelegateCommand(SendMessageCommand, () => !string.IsNullOrEmpty(OutgoingMessage) && !string.IsNullOrEmpty(_loginTo));
            eventAggregator.GetEvent<NewClientChosenViewModel>().Subscribe(HandleUserLoginTo);
            eventAggregator.GetEvent<PassLoginViewModel>().Subscribe(HandleUserLoginFrom);
            _chatPanelController = chatPanelController ?? throw new ArgumentNullException(nameof(chatPanelController));
            _chatPanelController.MessageReceivedEvent += HandleNewMessageReceived;
            _chatPanelController.OkReceivedEvent += HandleOkReceive;
        }

        #endregion

        #region Methods

        private void HandleNewMessageReceived(MessageReceivedEventArgs state)
        {
            _loginTo = state.FromClientName;
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(
                    () =>
                    {
                        ReceivedMessagesList.Add(new SendMessageViewModel(_loginTo, state.Message, state.Date));
                    }));
        }

        private void HandleUserLoginTo(string loginTo)
        {
            _loginTo = loginTo;
        }

        private void HandleUserLoginFrom(string loginFrom)
        {
            _loginFrom = loginFrom;
        }

        private void HandleOkReceive((MsgType, string) okReceive)
        {
            foreach (SendMessageViewModel message in ReceivedMessagesList)
            {
                if (message.Time == okReceive.Item2)
                {
                    switch (okReceive.Item1)
                    {
                        case MsgType.ServerOk:
                            message.ServerOkStatus = "Gray";
                            break;

                        case MsgType.ClientOk:
                            message.ClientOkStatus = "Green";
                            break;
                    }
                }
            }
        }

        private void SendMessageCommand()
        {
            var msgRequest = new TextMsgRequest(_loginFrom, _loginTo, OutgoingMessage);
            ReceivedMessagesList.Add(new SendMessageViewModel(_loginFrom, OutgoingMessage, msgRequest.Date));

            _chatPanelController.MessageSend(OutgoingMessage, _loginTo);
            OutgoingMessage = string.Empty;
        }

        #endregion
    }
}
