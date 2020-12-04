// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using Prism.Mvvm;

    public class SendMessageViewModel : BindableBase
    {
        #region Fields

        private string _serverOkStatus;
        private string _clientOkStatus;

        #endregion

        #region Properties

        public string Name { get; }
        public string Text { get; }
        public string Time { get; }

        public string ServerOkStatus
        {
            get => _serverOkStatus;
            set => SetProperty(ref _serverOkStatus, value);
        }

        public string ClientOkStatus
        {
            get => _clientOkStatus;
            set => SetProperty(ref _clientOkStatus, value);
        }

        #endregion

        #region Constructors

        public SendMessageViewModel(string name, string text, string time)
        {
            Name = name;
            Text = text;
            Time = time;
            ServerOkStatus = "white";
            ClientOkStatus = "white";
        }

        #endregion
    }
}
