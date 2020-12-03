// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.Messages
{
    using System.Collections.Generic;

    using Enums;

    public class UpdatedClientsResponse : BaseContainer
    {
        #region Fields

        public List<string> UsersList = new List<string>();

        #endregion

        #region Constructors

        public UpdatedClientsResponse(ICollection<string> usersList)
        {
            MessageType = MsgType.UpdatedClientsList;

            foreach (string user in usersList)
            {
                UsersList.Add(user);
            }
        }

        #endregion
    }
}
