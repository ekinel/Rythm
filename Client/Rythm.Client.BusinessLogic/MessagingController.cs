// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic
{
    using System;

    using Common.Network;
    using Common.Network.Enums;
    using Common.Network.Messages;

    public class MessagingController : IMessagingController
    {
        #region Fields

        private readonly IConnectionController _connectionServiceController;
        private readonly ITransport _currentTransport;

        #endregion

        #region Properties

        private string LoginTo { get; set; }

        #endregion

        #region Events

        public event Action<string> MessageReceivedEvent;

        #endregion

        #region Constructors

        public MessagingController(IConnectionController connectionServiceController)
        {
            _currentTransport = connectionServiceController.CurrentTransport ?? throw new ArgumentNullException(nameof(connectionServiceController));
            _connectionServiceController = connectionServiceController;
            _currentTransport.MessageReceived += HandleMessageReceived;
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

        #endregion
    }
}
