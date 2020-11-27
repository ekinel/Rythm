// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic
{
    using System;

    using Common.Network;

    public class MessagingController : IMessagingController
    {
        #region Fields

        private readonly IConnectionController _connectionServiceController;
        private readonly IUserLoginDisplayController _userLoginDisplayController;
        private readonly ITransport _currentTransport;

        #endregion

        #region Properties

        private string Login { get; set; }
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
            _currentTransport.MessageReceived += HandleUserLogin;

            _userLoginDisplayController = userLoginDisplayController;
            _userLoginDisplayController.NewUserLoginEvent += HandleNewLoginTo;
        }

        #endregion

        #region Methods

        public void MessageSend(string currentMessage)
        {
            var _msgContainer = new TextMsgContainer(Login, LoginTo, currentMessage);
            _currentTransport?.Send(_msgContainer);
        }

        private void HandleMessageReceived(object sender, MessageReceivedEventArgs state)
        {
            MessageReceivedEvent?.Invoke(state.Message);
        }

        private void HandleUserLogin(object sender, MessageReceivedEventArgs state)
        {
            UserLoginEvent?.Invoke(state.ToClientName);
            Login = state.FromClientName;
        }

        private void HandleNewLoginTo(string loginTo)
        {
            LoginTo = loginTo;
        }

        #endregion
    }
}
