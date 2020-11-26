// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic
{
    using System;

    public class UserLoginDisplayController : IUserLoginDisplayController
    {
        #region Fields

        private string _login;
        private bool _connectionParametersViewSettingsVisibility;

        #endregion

        #region Properties

        public string Login
        {
            get => _login;
            set
            {
                _login = value ?? throw new ArgumentNullException(nameof(value));
                NewUserLoginEvent(_login);
            }
        }

        public bool ConnectionParametersViewSettingsVisibility
        {
            get => _connectionParametersViewSettingsVisibility;
            set
            {
                _connectionParametersViewSettingsVisibility = value;
                NewParametersViewVisibilityEvent(_connectionParametersViewSettingsVisibility);
            }
        }

        #endregion

        #region Events

        public event Action<string> NewUserLoginEvent;
        public event Action<bool> NewParametersViewVisibilityEvent;

        #endregion
    }
}
