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
        private readonly IUserLoginDisplayController _userLoginDisplayController;
        private readonly ITransport _currentTransport;

        #endregion

        #region Properties

        private string LoginTo { get; set; }

        #endregion

        #region Events

        public event Action<string> MessageReceivedEvent;
        public event Action<string> UserLoginEvent;

        #endregion

        #region Constructors

        public MessagingController(IConnectionController connectionServiceController, IUserLoginDisplayController userLoginDisplayController)
        {
            _currentTransport = connectionServiceController.CurrentTransport ?? throw new ArgumentNullException(nameof(connectionServiceController));
            _connectionServiceController = connectionServiceController;
            _currentTransport.MessageReceived += HandleMessageReceived;

            _userLoginDisplayController = userLoginDisplayController;
            _userLoginDisplayController.NewUserLoginEvent += HandleNewLoginTo;
        }

        #endregion

        #region Methods

        public void MessageSend(string currentMessage)
        {
            var msgContainer = new TextMsgContainer(_connectionServiceController.Login, LoginTo, currentMessage);
            var mr = new MessageRequest(msgContainer, MsgType.PersonalMessage);
            _currentTransport?.Send(mr);
        }

        private void HandleMessageReceived(object sender, MessageReceivedEventArgs state)
        {
            MessageReceivedEvent?.Invoke(state.Message);
        }

        private void HandleNewLoginTo(string loginTo)
        {
            LoginTo = loginTo;
        }

        #endregion
    }
}
