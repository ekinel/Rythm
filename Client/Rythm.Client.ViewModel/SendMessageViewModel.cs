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

        public string LoginTo { get; }
        public string Text { get; set; }
        public string Time { get; set; }

        public string LoginFrom { get;}

        public string OkStatus
        {
            get => _okStatus;
            set => SetProperty(ref _okStatus, value);
        }

        #endregion

        #region Constructors

        public SendMessageViewModel(string loginTo, string loginFrom, string text, string time)
        {
            LoginTo = loginTo;
            Text = text;
            Time = time;
            OkStatus = "white";
            LoginFrom = loginFrom;
        }

        #endregion
    }
}
