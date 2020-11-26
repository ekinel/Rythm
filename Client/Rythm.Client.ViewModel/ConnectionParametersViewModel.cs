// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using System;
    using System.Windows.Input;

    using BusinessLogic;

    using Prism.Commands;
    using Prism.Mvvm;

    public class ConnectionParametersViewModel : BindableBase
    {
        #region Fields

        private readonly IConnectionController _connectionController;

        private string _address = "127.0.0.1";

        private bool _fieldsEnabled = true;
        private string _login;
        private string _port = "65000";

        private string _viewVisibility = "hidden";

        #endregion

        #region Properties

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

        public bool FieldsEnabled
        {
            get => _fieldsEnabled;
            set => SetProperty(ref _fieldsEnabled, value);
        }

        public string ViewVisibility
        {
            get => _viewVisibility;
            set => SetProperty(ref _viewVisibility, value);
        }

        #endregion

        #region Constructors

        public ConnectionParametersViewModel(IConnectionController connectionController)
        {
            _connectionController = connectionController ?? throw new ArgumentNullException(nameof(connectionController));
            _connectionController.SendNewStateParametersViewVisibility += HandleConnectionParametersViewVisibility;
            LoginCommand = new DelegateCommand(LoginUserCommand);
        }

        #endregion

        #region Methods

        private void LoginUserCommand()
        {
            FieldsEnabled = false;
            _connectionController.DataSending(Address, Port, Login);
        }

        private void HandleConnectionParametersViewVisibility(string visibility)
        {
            ViewVisibility = visibility;
        }

        #endregion
    }
}
