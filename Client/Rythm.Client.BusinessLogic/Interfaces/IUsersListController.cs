// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic.Interfaces
{
    using System;
    using System.Collections.Generic;

    public interface IUsersListController
    {
        event Action<List<string>> UpdatedUsersListEvent;

    }
}
