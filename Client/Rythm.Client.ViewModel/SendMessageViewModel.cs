// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using Prism.Mvvm;

    public class SendMessageViewModel : BindableBase
    {
        #region Fields

        #endregion

        #region Properties

        public string Name { get; }

        public string Text { get; }

        #endregion

        #region Constructors

        public SendMessageViewModel(string name, string text)
        {
            Name = name;
            Text = text;
        }

        #endregion
    }
}
