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

        public List<string> ActiveUsersList = new List<string>();
        public List<string> NotActiveUsersList = new List<string>();

        #endregion

        #region Constructors

        public UpdatedClientsResponse(ICollection<string> activeUsersList, ICollection<string> notActiveUsersList)
        {
            MessageType = MsgType.UpdatedClientsList;

            foreach (string user in activeUsersList)
            {
	            ActiveUsersList.Add(user);
            }

            foreach (string user in notActiveUsersList)
            {
	            NotActiveUsersList.Add(user);
            }
        }

        #endregion
    }
}
