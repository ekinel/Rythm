// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using System;
    using System.Windows.Input;

    using BusinessLogic.Interfaces;

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

        #endregion

        #region Constructors

        public UsersLoginsViewModel(IEventAggregator eventAggregator, string login)
        {
            _eventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            ChooseLoginCommand = new DelegateCommand(ChosenLogin);
            Login = login;
        }

        #endregion

        #region Methods

        private void ChosenLogin()
        {
            _eventAggregator.GetEvent<NewClientChosenViewModel>().Publish(Login);
        }

        #endregion
    }
}
