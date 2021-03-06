﻿// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic
{
    using System;
    using System.Collections.Generic;

    using Common.Network;
    using Common.Network.Enums;
    using Common.Network.Messages;

    using Newtonsoft.Json.Linq;

    public class ChatPanelController : IChatPanelController
    {
        #region Fields

        private readonly IConnectionController _connectionServiceController;
        private readonly ITransport _currentTransport;

        private List<string> UpdatedUsersList = new List<string>();

        #endregion

        #region Properties

        private string LoginTo { get; set; }

        #endregion

        #region Events

        public event Action<string> MessageReceivedEvent;
        public event Action<List<string>> UpdatedUsersListEvent;

        #endregion

        #region Constructors

        public ChatPanelController(IConnectionController connectionServiceController)
        {
            _currentTransport = connectionServiceController.CurrentTransport ?? throw new ArgumentNullException(nameof(connectionServiceController));
            _connectionServiceController = connectionServiceController;
            _currentTransport.MessageReceived += HandleMessageReceived;
            _currentTransport.UpdatedUsersList += HandleUpdatedUsersList;
        }

        #endregion

        #region Methods

        public void MessageSend(string currentMessage, string loginTo)
        {
            var msgContainer = new TextMsgRequest(_connectionServiceController.Login, loginTo, currentMessage);
            var mr = new MessageRequest(msgContainer, MsgType.PersonalMessage);
            _currentTransport?.Send(mr);
        }

        private void HandleMessageReceived(object sender, MessageReceivedEventArgs state)
        {
            MessageReceivedEvent?.Invoke(state.Message);
        }

        private void HandleUpdatedUsersList(object sender, MessageContainer msgContainer)
        {
            var messageRequest = ((JObject)msgContainer.Payload).ToObject(typeof(UpdatedClientsResponse)) as UpdatedClientsResponse;
            var UpdatedUsersList = messageRequest.UsersList;
            UpdatedUsersListEvent?.Invoke(UpdatedUsersList);
        }

        #endregion
    }
}
