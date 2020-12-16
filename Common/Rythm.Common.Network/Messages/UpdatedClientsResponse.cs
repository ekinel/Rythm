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

        public readonly List<string> ActiveUsersList = new List<string>();
        public readonly List<string> NotActiveUsersList = new List<string>();

        #endregion

        #region Constructors

        public UpdatedClientsResponse(IEnumerable<string> activeUsersList, IEnumerable<string> notActiveUsersList)
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
