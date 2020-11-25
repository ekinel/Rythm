// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic
{
    using System;

    public class UserLoginDisplayController : IUserLoginDisplayController
    {
        #region Properties

        public string Login
        {
            get => Login;
            set
            {
                Login = value ?? throw new ArgumentNullException(nameof(value));
                NewUserLoginEvent(Login);
            }
        } 

        #endregion

        #region Events

        public event Action<string> NewUserLoginEvent;

        #endregion
    }
}
