// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
	using System;

	using Prism.Mvvm;
	using Rythm.Common.Network.Enums;

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

        public SendMessageViewModel(string loginTo, string loginFrom, string text, DateTime time, MsgStatus okStatus)
        {
            LoginTo = loginTo;
            Text = text;
            Time = time;
            LoginFrom = loginFrom;

			switch (okStatus)
            {
                case MsgStatus.ServerOk:
                    OkStatus = "Gray";
                    break;

                case MsgStatus.ClientOk:
                    OkStatus = "Green";
                    break;

                default:
                    OkStatus = "White";
					break;
			};

		}

        public SendMessageViewModel(string loginTo, string loginFrom, string text, DateTime time, string okStatus)
        {
            LoginTo = loginTo;
            Text = text;
            Time = time;
            LoginFrom = loginFrom;

            switch (okStatus)
            {
                case "ServerOk":
                case "Gray":
                    OkStatus = "Gray";
                    break;

                case "ClientOk":
                case "Green":
                    OkStatus = "Green";
                    break;

                default:
                    OkStatus = "White";
                    break;
            };
        }
        #endregion
    }
}
