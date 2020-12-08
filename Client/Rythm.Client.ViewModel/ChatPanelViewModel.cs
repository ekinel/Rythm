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
        private string _loginTo = string.Empty;
        private string _loginFrom;

        #endregion

        #region Properties

        public DelegateCommand SendCommand { get; }

        public ObservableCollection<SendMessageViewModel> ReceivedMessagesList { get; set; } = new ObservableCollection<SendMessageViewModel>();
        public ObservableCollection<SendMessageViewModel> AllReceivedMessagesList { get; set; } = new ObservableCollection<SendMessageViewModel>();

        public string OutgoingMessage
        {
            get => _outgoingMessage;
            set
            {
                SetProperty(ref _outgoingMessage, value);
                SendCommand.RaiseCanExecuteChanged();
            }
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
            AllReceivedMessagesList.Add(new SendMessageViewModel(state.FromClientName, _loginFrom, state.Message, state.Date));

            UpdateListByNewLoginTo();
        }

        private void HandleUserLoginTo(string loginTo)
        {
            _loginTo = loginTo;
            SendCommand.RaiseCanExecuteChanged();
            UpdateListByNewLoginTo();
        }

        private void HandleUserLoginFrom(string loginFrom)
        {
            _loginFrom = loginFrom;
        }

        private void UpdateListByNewLoginTo()
        {
            Application.Current.Dispatcher.BeginInvoke(
                DispatcherPriority.Background,
                new Action(
                    () =>
                    {
                        ReceivedMessagesList.Clear();
                    }));

            foreach (SendMessageViewModel message in AllReceivedMessagesList)
            {
                if (message.LoginFrom == _loginTo && message.LoginTo == _loginFrom ||
                    message.LoginFrom == _loginFrom && message.LoginTo == _loginTo && _loginTo != string.Empty)
                {
                    Application.Current.Dispatcher.BeginInvoke(
                        DispatcherPriority.Background,
                        new Action(
                            () =>
                            {
                                ReceivedMessagesList.Add(message);
                            }));
                }
            }
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
                            message.OkStatus = "Gray";
                            break;

                        case MsgType.ClientOk:
                            message.OkStatus = "Green";
                            break;
                    }
                }
            }
        }

        private void SendMessageCommand()
        {
            var msgRequest = new TextMsgRequest(_loginFrom, _loginTo, OutgoingMessage);
            AllReceivedMessagesList.Add(new SendMessageViewModel(_loginFrom, _loginTo, OutgoingMessage, msgRequest.Date));
            _chatPanelController.MessageSend(OutgoingMessage, _loginTo);
            OutgoingMessage = string.Empty;

            UpdateListByNewLoginTo();
        }

        #endregion
    }
}
