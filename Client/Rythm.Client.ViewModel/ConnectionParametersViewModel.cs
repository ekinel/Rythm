namespace Rythm.Client.ViewModel
{
    using System;
    using System.Windows.Input;

    using Prism.Mvvm;
    using Prism.Commands;

    using Rythm.Client.BusinessLogic;

    public class ConnectionParametersViewModel : BindableBase
    {
        private readonly IConnectionServiceController _connectionServiceController;

        private string _address;
        private string _port;
        private string _login;

        private bool _fieldsVisibility = true;
        public ICommand LoginCommand { get; }

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

        public bool FieldsVisibility
        {
            get => _fieldsVisibility;
            set => SetProperty(ref _fieldsVisibility, value);
        }

        public ConnectionParametersViewModel(IConnectionServiceController connectionService)
        {
            _connectionServiceController = connectionService ?? throw new ArgumentNullException(nameof(connectionService));
            LoginCommand = new DelegateCommand(LoginUserCommand);
        }
        private void LoginUserCommand()
        {
            FieldsVisibility = false;
            _connectionServiceController.DataSending(Address, Port, Login);
        }
    }
}
