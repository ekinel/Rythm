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

        private string _okColorStatus;
        private int _opacity;

        #endregion

        #region Properties

        public string LoginTo { get; }
        public string Text { get; set; }
        public DateTime Time { get; set; }

        public string LoginFrom { get;}

        public string OkColorStatus
        {
            get => _okColorStatus;
            set => SetProperty(ref _okColorStatus, value);
        }

        public int OkColorStatusOpacity
        {
            get => _opacity;
            set => SetProperty(ref _opacity, value);
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
                    OkColorStatus = "Green";
                    OkColorStatusOpacity = 1;
                    break;

                case MsgStatus.ClientOk:
                    OkColorStatus = "Green";
                    OkColorStatusOpacity = 1;
                    break;

                default:
                    OkColorStatus = "White";
                    OkColorStatusOpacity = 0;
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
                    OkColorStatus = "Green";
                    OkColorStatusOpacity = 1;
                    break;

                case "ClientOk":
                case "Green":
                    OkColorStatus = "Green";
                    OkColorStatusOpacity = 1;
                    break;

                default:
                    OkColorStatus = "White";
                    OkColorStatusOpacity = 0;
                    break;
            };
        }
        #endregion
    }
}
