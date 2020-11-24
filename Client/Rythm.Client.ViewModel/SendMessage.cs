﻿// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using Prism.Mvvm;

    public class SendMessage : BindableBase
    {
        #region Fields

        #endregion

        #region Properties

        public string Name { get; }

        public string Text { get; }

        #endregion

        #region Constructors

        public SendMessage(string name, string text)
        {
            Name = name;
            Text = text;
        }

        #endregion
    }
}
