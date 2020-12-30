// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using System;
    using System.Windows.Input;

    using Events;

    using Prism.Commands;
    using Prism.Events;

    public class UsersLoginsViewModel
    {
        #region Fields

        private readonly IEventAggregator _eventAggregator;

        #endregion

        #region Properties

        public string Login { get; }
        public ICommand ChooseLoginCommand { get; }

        public bool IsEnabled { get; }

        #endregion

        #region Constructors

        public UsersLoginsViewModel(IEventAggregator eventAggregator, string login, bool isEnabled)
        {
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            ChooseLoginCommand = new DelegateCommand(ExecuteChosenLogin);
            Login = login;
            IsEnabled = isEnabled;
        }

        #endregion

        #region Methods

        private void ExecuteChosenLogin()
        {
            _eventAggregator.GetEvent<NewClientChosenViewModel>().Publish(Login);
        }

        #endregion
    }
}
