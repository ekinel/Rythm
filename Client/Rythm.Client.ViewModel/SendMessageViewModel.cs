// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
	using System;

	using Prism.Mvvm;

    public class SendMessageViewModel : BindableBase
    {
        #region Fields

        private string _okStatus;

        #endregion

        #region Properties

        public string LoginTo { get; }
        public string Text { get; set; }
        public DateTime Time { get; set; }

        public string LoginFrom { get;}

        public string OkStatus
        {
            get => _okStatus;
            set => SetProperty(ref _okStatus, value);
        }

        #endregion

        #region Constructors

        public SendMessageViewModel(string loginTo, string loginFrom, string text, DateTime time, string okStatus = "White")
        {
            LoginTo = loginTo;
            Text = text;
            Time = time;
            LoginFrom = loginFrom;

			switch (okStatus)
            {
                case "ServerOk":
                    OkStatus = "gray";
                    break;

                case "ClientOk":
                    OkStatus = "green";
                    break;

                default:
                    OkStatus = okStatus;
					break;
			};
		}
		#endregion
	}
}
