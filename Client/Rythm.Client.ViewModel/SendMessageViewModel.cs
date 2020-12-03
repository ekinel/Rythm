// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
    using Prism.Mvvm;

    public class SendMessageViewModel : BindableBase
    {
        #region Fields

        private bool _serverOkStatus;
        private bool _clientOkStatus;

        #endregion

        #region Properties

        public string Name { get; }
        public string Text { get; }
        public string Time { get; }

        public bool ServerOkStatus
        {
            get => _serverOkStatus;
            set => SetProperty(ref _serverOkStatus, value);
        }

        public bool ClientOkStatus
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
            ServerOkStatus = false;
            ClientOkStatus = false;
        }

        #endregion
    }
}
