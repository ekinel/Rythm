﻿// ---------------------------------------------------------------------------------------------------------------------------------------------------
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
    using Common.Network.Messages;

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
            _chatPanelController.ServerOkReceivedEvent += HandleServerOkReceive;
            _chatPanelController.ClientOkReceivedEvent += HandleClientOkReceive;
        }

        #endregion

        #region Methods

        public void HandleNewMessageReceived(MessageReceivedEventArgs state)
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

        public void HandleUserLoginTo(string loginTo)
        {
            _loginTo = loginTo;
        }

        public void HandleUserLoginFrom(string loginFrom)
        {
            _loginFrom = loginFrom;
        }

        public void HandleServerOkReceive(ServerOkMsgResponse msgResponse)
        {
            foreach (var message in ReceivedMessagesList)
            {
                if (message.Time == msgResponse.Date)
                {
                    message.ServerOkStatus = true;

                }
            }
        }

        public void HandleClientOkReceive(ClientOkMsgResponse msgResponse)
        {
            foreach (var message in ReceivedMessagesList)
            {
                if (message.Time == msgResponse.Date)
                {
                    message.ClientOkStatus = true;

                }
            }
        }

        private void SendMessageCommand()
        {
            _chatPanelController.MessageSend(OutgoingMessage, _loginTo);
            var msgRequest = new TextMsgRequest(_loginFrom, _loginTo, OutgoingMessage);
            HandleNewMessageReceived(new MessageReceivedEventArgs(msgRequest));
            OutgoingMessage = string.Empty;
        }

        #endregion
    }
}
