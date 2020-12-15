// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
	using System;
	using System.Text.RegularExpressions;

	using BusinessLogic.Interfaces;

	using Events;

	using Prism.Commands;
	using Prism.Events;

	public class ConnectionParametersViewModel : ErrorsCheckViewModel
	{
		#region Constants

		private const string ADDRESS = "127.0.0.1";
		private const string PORT = "65000";
		private const string ADDRESS_PATTERN = @"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}$";
		private const string STATE_CONNECT = "Connect";
		private const string STATE_DISCONNECT = "Disconnect";
		private const int MIN_PORT = 49152;
		private const int MAX_PORT = 65535;


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
				if (SetProperty(ref _address, value))
				{
					CheckAddress();
				}
			}
		}

		public string Port
		{
			get => _port;
			set
			{
				if (SetProperty(ref _port, value))
				{
					CheckPort();
				}
			}
		}

		public string Login
		{
			get => _login;
			set
			{
				if (SetProperty(ref _login, value))
				{
					CheckLogin();
				}
			}
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
			ConnectCommand = new DelegateCommand(ExecuteConnectCommand, () => !_errorsContainer.HasErrors);
			_connectionController.ConnectionStateChanged += HandleConnectionStateChanged;
			ConnectButtonLabel = STATE_CONNECT;
			CheckLogin();
			CheckAddress();
			CheckPort();
		}

		#endregion

		#region Methods

		private void CheckLogin()
		{
			_errorsContainer.ClearErrors(() => Login);

			if (string.IsNullOrEmpty(Login))
			{
				_errorsContainer.SetErrors(() => Login, new[] { "Login cannot be empty" });
			}

			ConnectCommand.RaiseCanExecuteChanged();
		}

		private void CheckPort()
		{
			_errorsContainer.ClearErrors(() => Port);
			bool isNumber = int.TryParse(Port, out int portNumber);
			if (!isNumber || !(portNumber >= MIN_PORT && portNumber <= MAX_PORT))
			{
				_errorsContainer.SetErrors(() => Port, new[] { $"The port value must be in the range from {MIN_PORT} to {MAX_PORT}" });
			}

			ConnectCommand.RaiseCanExecuteChanged();
		}

		private void CheckAddress()
		{
			_errorsContainer.ClearErrors(() => Address);
			var regex = new Regex(ADDRESS_PATTERN);
			Match compare = regex.Match(Address);

			if (!compare.Success)
			{
				_errorsContainer.SetErrors(() => Address, new[] { "The address must match the pattern of ip addresses" });
			}

			ConnectCommand.RaiseCanExecuteChanged();
		}

		private void HandleConnectionStateChanged(bool isConnected)
		{
			_isConnected = isConnected;
			FieldsEnabled = !_isConnected;
			ConnectButtonLabel = _isConnected ? STATE_DISCONNECT : STATE_CONNECT;
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
