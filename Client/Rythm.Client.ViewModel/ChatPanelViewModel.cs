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
	using Common.Network.Enums;
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
				SendMessageCommand,
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
				AllReceivedMessagesList.Add(new SendMessageViewModel(message.ClientTo, message.ClientFrom, message.Text, message.Date));
			}
		}

		private void HandleNewMessageReceived(MessageReceivedEventArgs state)
		{
			AllReceivedMessagesList.Add(new SendMessageViewModel(state.ToClientName, state.FromClientName, state.Message, state.Date));

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
				     message.LoginFrom == _loginFrom && message.LoginTo == _loginTo || message.LoginTo == "CommonChat" && _loginTo == "CommonChat") &&
				    _loginTo != string.Empty)
				{
					ReceivedMessagesList.Add(
						new SendMessageViewModel(message.LoginTo, message.LoginFrom, message.Text, message.Time)
						{
							OkStatus = message.OkStatus
						});
				}
			}
		}

		private void HandleOkReceive((string, DateTime) okReceive)
		{
			foreach (SendMessageViewModel message in AllReceivedMessagesList)
			{
				if (message.Time == okReceive.Item2 && message.LoginTo != "CommonChat")
				{
					switch (okReceive.Item1)
					{
						case "ServerOk":
							if (message.OkStatus != "Green")
							{
								message.OkStatus = "Gray";
							}

							break;

						case "ClientOk":
							message.OkStatus = "Green";
							break;
					}
				}
			}

			UpdateListByNewLoginTo();
		}

		private void SendMessageCommand()
		{
			var msgRequest = new TextMsgRequest(_loginFrom, _loginTo, OutgoingMessage, "None");
			AllReceivedMessagesList.Add(new SendMessageViewModel(_loginTo, _loginFrom, OutgoingMessage, msgRequest.Date));
			_chatPanelController.MessageSend(OutgoingMessage, _loginTo);
			OutgoingMessage = string.Empty;

			UpdateListByNewLoginTo();
		}

		#endregion
	}
}
