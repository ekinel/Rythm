// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;

	using BusinessLogic.Interfaces;

	using Common.Network;
	using Common.Network.Messages;

	using Events;

	using Prism.Commands;
	using Prism.Events;
	using Prism.Mvvm;
	using Rythm.Common.Network.Enums;

	public class ChatPanelViewModel : BindableBase
	{
		#region Fields

		private readonly IChatPanelController _chatPanelController;

		private string _outgoingMessage;
		private string _loginTo = string.Empty;
		private string _loginFrom;

		private bool _connectionState;

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

		public ChatPanelViewModel(
			IChatPanelController chatPanelController,
			IEventAggregator eventAggregator,
			IDisplayingEventsController eventsController)
		{
			SendCommand = new DelegateCommand(
				ExecuteSendMessageCommand,
				() => !string.IsNullOrEmpty(OutgoingMessage) && !string.IsNullOrEmpty(_loginTo) && _connectionState);
			eventAggregator.GetEvent<NewClientChosenViewModel>().Subscribe(HandleUserLoginTo);
			eventAggregator.GetEvent<PassLoginViewModel>().Subscribe(HandleUserLoginFrom);
			eventAggregator.GetEvent<ConnectionIndicatorColorChangedEvent>().Subscribe(HandleNewSateEstablished);
			_chatPanelController = chatPanelController ?? throw new ArgumentNullException(nameof(chatPanelController));
			_chatPanelController.MessageReceivedEvent += HandleNewMessageReceived;
			_chatPanelController.OkReceivedEvent += HandleOkReceive;
			eventsController.UpdatedDataBaseMessagesEvent += HandleDownloadMessagesList;
		}

		#endregion

		#region Methods

		private void HandleDownloadMessagesList(List<DataBaseMessage> messagesList)
		{
			AllReceivedMessagesList.Clear();
			foreach (DataBaseMessage message in messagesList)
			{
				AllReceivedMessagesList.Add(new SendMessageViewModel(message.ClientTo, message.ClientFrom, message.Text, message.Date, message.MsgStatus));
			}
		}

		private void HandleNewMessageReceived(MessageReceivedEventArgs state)
		{
			AllReceivedMessagesList.Add(new SendMessageViewModel(state.ToClientName, state.FromClientName, state.Message, state.Date, state.Status));

			UpdateListByNewLoginTo();
		}

		private void HandleNewSateEstablished(bool connectionState)
		{
			_connectionState = connectionState;
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
			ReceivedMessagesList.Clear();

			foreach (SendMessageViewModel message in AllReceivedMessagesList)
			{
				if ((message.LoginFrom == _loginTo && message.LoginTo == _loginFrom ||
				     message.LoginFrom == _loginFrom && message.LoginTo == _loginTo || message.LoginTo == Properties.Resources.CommonChat && _loginTo == Properties.Resources.CommonChat) &&
				    _loginTo != string.Empty)
				{
					if(message.LoginTo == "CommonChat" || message.LoginFrom != _loginFrom)
					{
						ReceivedMessagesList.Add(
							new SendMessageViewModel(message.LoginTo, message.LoginFrom, message.Text, message.Time, MsgStatus.None));
					}
					else
					{
						ReceivedMessagesList.Add(
							new SendMessageViewModel(message.LoginTo, message.LoginFrom, message.Text, message.Time, message.OkColorStatus));
					}
				}
			}
		}

		private void HandleOkReceive(OkReceiveMsg okReceive)
		{
			foreach (SendMessageViewModel message in AllReceivedMessagesList)
			{
				if (message.Time == okReceive.MsgTime && message.LoginTo != Properties.Resources.CommonChat)
				{
					if(okReceive.MsgStatus == MsgStatus.ServerOk)
					{
						message.OkColorStatus = "Gray";
					}
					else
					{
						message.OkColorStatus = "Green";

					}
				}
			}

			UpdateListByNewLoginTo();
		}

		private void ExecuteSendMessageCommand()
		{
			var msgRequest = new TextMsgRequest(_loginFrom, _loginTo, OutgoingMessage, MsgStatus.None);
			AllReceivedMessagesList.Add(new SendMessageViewModel(_loginTo, _loginFrom, OutgoingMessage, msgRequest.Date, MsgStatus.None));
			_chatPanelController.MessageSend(OutgoingMessage, _loginTo);
			OutgoingMessage = string.Empty;

			UpdateListByNewLoginTo();
		}

		#endregion
	}
}
