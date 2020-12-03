// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.BusinessLogic
{
    using System;

    public interface IToolPanelDisplayController
    {
        #region Properties

        string Login { get; set; }
        bool ConnectionParametersViewSettingsVisibility { get; set; }

        #endregion

        #region Events

        event Action<string> NewUserLoginEvent;
        event Action<bool> NewParametersViewVisibilityEvent;

        #endregion
    }
}
