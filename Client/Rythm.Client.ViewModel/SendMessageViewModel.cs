// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using Prism.Mvvm;

    public class SendMessageViewModel : BindableBase
    {
        #region Properties

        public string Name { get; }
        public string Text { get; }
        public string Time { get; }

        #endregion

        #region Constructors

        public SendMessageViewModel(string name, string text, string time)
        {
            Name = name;
            Text = text;
            Time = time;
        }

        #endregion
    }
}
