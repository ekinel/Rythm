// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using System;
    using System.Windows.Input;

    using BusinessLogic;

    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;

    public class ConnectionParametersViewModel : BindableBase
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

        public ICommand ConnectCommand { get; }

        public string ConnectButtonLabel
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        public string Port
        {
            get => _port;
            set => SetProperty(ref _port, value);
        }

        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        public bool FieldsEnabled
        {
            get => _fieldsEnabled;
            set => SetProperty(ref _fieldsEnabled, value);
        }

        #endregion

        #region Constructors

        public ConnectionParametersViewModel(IConnectionController connectionController, IEventAggregator eventAggregator)
        {
            _connectionController = connectionController ?? throw new ArgumentNullException(nameof(connectionController));
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            ConnectCommand = new DelegateCommand(ExecuteConnectCommand);
            _connectionController.ConnectionStateChanged += HandleConnectionStateChanged;
            ConnectButtonLabel = "Connect";
        }

        #endregion

        #region Methods

        private void HandleConnectionStateChanged(bool isConnected)
        {
            _isConnected = isConnected;
            FieldsEnabled = !_isConnected;
            ConnectButtonLabel = _isConnected ? "Disconnect" : "Connect";
            _eventAggregator.GetEvent<ConnectionIndicatorColorChangedEvent>().Publish(_isConnected);
        }

        private void ExecuteConnectCommand()
        {
            if (Login.Length != 0 && Port.Length != 0 && Address.Length != 0)
            {
                if (_isConnected)
                {
                    _connectionController.DoDisconnect();
                }
                else
                {
                    _connectionController.DoConnect(Address, Port, Login);
                }
            }
        }

        #endregion
    }
}
