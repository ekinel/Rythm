// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using Prism.Mvvm;

    public class SendMessageViewModel : BindableBase
    {
        #region Fields

        private string _okStatus;

        #endregion

        #region Properties

        public string Name { get; }
        public string Text { get; }
        public string Time { get; }

        public string OkStatus
        {
            get => _okStatus;
            set => SetProperty(ref _okStatus, value);
        }

        #endregion

        #region Constructors

        public SendMessageViewModel(string name, string text, string time)
        {
            Name = name;
            Text = text;
            Time = time;
            OkStatus = "white";
        }

        #endregion
    }
}
