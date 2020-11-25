// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic
{
    public class UserListController : IUserListController
    {
        #region Fields

        private readonly IUserLoginDisplayController _userLoginDisplayController;

        #endregion

        #region Constructors

        public UserListController(IUserLoginDisplayController userLoginDisplayController)
        {
            _userLoginDisplayController = userLoginDisplayController;
        }

        #endregion

        #region Methods

        public void SendUserLogin(string login)
        {
            _userLoginDisplayController.Login = login;
        }

        #endregion
    }
}
