// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using System;
    using System.Text.RegularExpressions;

    using BusinessLogic;

    using Events;

    using Prism.Commands;
    using Prism.Events;

    public class ConnectionParametersViewModel : ErrorsCheckViewModel
    {
        #region Constants

        private const string ADDRESS = "127.0.0.1";
        private const string PORT = "65000";

        #endregion

        #region Fields

        private readonly IConnectionController _connectionController;

        private string _address = ADDRESS;

        private bool _isConnected;
        private string _login;
        private string _port = PORT;

        private string _label;
        private bool _fieldsEnabled = true;
        private readonly IEventAggregator _eventAggregator;

        #endregion

        #region Properties

        public DelegateCommand ConnectCommand { get; }

        public string ConnectButtonLabel
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        public string Address
        {
            get => _address;
            set
            {
                SetProperty(ref _address, value);
                ConnectCommand.RaiseCanExecuteChanged();
                CheckAddress();
            }
        }

        public string Port
        {
            get => _port;
            set
            {
                SetProperty(ref _port, value);
                ConnectCommand.RaiseCanExecuteChanged();
                CheckPort();
            }
        }

        public string Login
        {
            get => _login;
            set
            {
                SetProperty(ref _login, value);
                ConnectCommand.RaiseCanExecuteChanged();
                CheckLogin();
            }
        }

        public bool FieldsEnabled
        {
            get => _fieldsEnabled;
            set => SetProperty(ref _fieldsEnabled, value);
        }

        private bool CorrectPort { get; set; } = true;
        private bool CorrectAddress { get; set; } = true;

        #endregion

        #region Constructors

        public ConnectionParametersViewModel(IConnectionController connectionController, IEventAggregator eventAggregator)
        {
            _connectionController = connectionController ?? throw new ArgumentNullException(nameof(connectionController));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            ConnectCommand = new DelegateCommand(
                ExecuteConnectCommand,
                () => !string.IsNullOrEmpty(Login) && CorrectPort && CorrectAddress);
            _connectionController.ConnectionStateChanged += HandleConnectionStateChanged;
            ConnectButtonLabel = "Connect";
        }

        #endregion

        #region Methods

        public sealed override void CheckLogin()
        {
            _errorsContainer.ClearErrors(() => Login);

            if (string.IsNullOrEmpty(Login))
            {
                _errorsContainer.SetErrors(() => Login, new[] { "Error in login" });
            }
        }

        public sealed override void CheckPort()
        {
            _errorsContainer.ClearErrors(() => Port);
            bool isNumber = int.TryParse(Port, out int number);
            if (!isNumber)
            {
                _errorsContainer.SetErrors(() => Port, new[] { "Error in port" });
                CorrectPort = false;
            }
            else
            {
                CorrectPort = true;
            }
        }

        public sealed override void CheckAddress()
        {
            _errorsContainer.ClearErrors(() => Address);
            string addressPattern = @"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}$";
            var regex = new Regex(addressPattern);
            Match compare = regex.Match(Address);

            if (!compare.Success)
            {
                _errorsContainer.SetErrors(() => Address, new[] { "Error in address" });
                CorrectAddress = false;
            }
            else
            {
                CorrectAddress = true;
            }
        }

        private void HandleConnectionStateChanged(bool isConnected)
        {
            _isConnected = isConnected;
            FieldsEnabled = !_isConnected;
            ConnectButtonLabel = _isConnected ? "Disconnect" : "Connect";
            _eventAggregator.GetEvent<ConnectionIndicatorColorChangedEvent>().Publish(_isConnected);
        }

        private void ExecuteConnectCommand()
        {
            if (_isConnected)
            {
                _connectionController.DoDisconnect();
            }
            else
            {
                _connectionController.DoConnect(Address, Port, Login);
                _eventAggregator.GetEvent<PassLoginViewModel>().Publish(Login);
            }
        }

        #endregion
    }
}
