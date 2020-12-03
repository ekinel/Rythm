// ---------------------------------------------------------------------------------------------------------------------------------------------------
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

        public event Action<MessageReceivedEventArgs> MessageReceivedEvent;
        public event Action<ServerOkMsgResponse> ServerOkReceivedEvent;
        public event Action<ClientOkMsgResponse> ClientOkReceivedEvent;
        public event Action<List<string>> UpdatedUsersListEvent;

        #endregion

        #region Constructors

        public ChatPanelController(IConnectionController connectionServiceController)
        {
            _currentTransport = connectionServiceController.CurrentTransport ?? throw new ArgumentNullException(nameof(connectionServiceController));
            _connectionServiceController = connectionServiceController;
            _currentTransport.MessageReceived += HandleMessageReceived;
            _currentTransport.UpdatedUsersList += HandleUpdatedUsersList;
            _currentTransport.ServerOkReceive += HandleServerOkReceived;
            _currentTransport.ClientOkReceive += HandleClientOkReceived;
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
            MessageReceivedEvent?.Invoke(state);
        }

        private void HandleUpdatedUsersList(object sender, MessageContainer msgContainer)
        {
            var messageRequest = ((JObject)msgContainer.Payload).ToObject(typeof(UpdatedClientsResponse)) as UpdatedClientsResponse;
            List<string> UpdatedUsersList = messageRequest.UsersList;
            UpdatedUsersListEvent?.Invoke(UpdatedUsersList);
        }

        private void HandleServerOkReceived(object sender, MessageContainer msgContainer)
        {
            var messageResponse = ((JObject)msgContainer.Payload).ToObject(typeof(ServerOkMsgResponse)) as ServerOkMsgResponse;
            ServerOkReceivedEvent(messageResponse);
        }

        private void HandleClientOkReceived(object sender, MessageContainer msgContainer)
        {
            var messageResponse = ((JObject)msgContainer.Payload).ToObject(typeof(ClientOkMsgResponse)) as ClientOkMsgResponse;
            ClientOkReceivedEvent(messageResponse);
        }

        #endregion
    }
}
