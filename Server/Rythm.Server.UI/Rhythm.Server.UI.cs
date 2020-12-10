// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.UI
{
    using System;
    using System.Windows.Forms;

    using Properties;

    using Service;

    public partial class RythmServerUI : Form
    {
        #region Constants

        private const int WS_PORT = 65000;
        private const int WS_TIMEOUT = 20;

        #endregion

        #region Fields

        private NetworkManager _networkManager;

        #endregion

        #region Constructors

        public RythmServerUI()
        {
            InitializeComponent();
            LabelServerStatus.Text = Resources.ServerStatusStop;
        }

        #endregion

        #region Methods

        private void ButtonStartClick(object sender, EventArgs e)
        {
            string wsPort = TextBoxPort.Text;
            string wsAddress = TextBoxAddress.Text;
            string wsTimeOut = TextBoxTimeOut.Text;

            if (!string.IsNullOrEmpty(wsPort))
            {
                ButtonStop.Enabled = true;
                ButtonStart.Enabled = false;

                TextBoxPort.Enabled = false;
                TextBoxAddress.Enabled = false;
                TextBoxTimeOut.Enabled = false;
                TextBoxDataBase.Enabled = false;

                try
                {
                    if (string.IsNullOrEmpty(wsPort))
                    {
                        wsPort = WS_PORT.ToString();
                    }

                    if (string.IsNullOrEmpty(wsTimeOut))
                    {
                        wsTimeOut = WS_TIMEOUT.ToString();
                    }

                    _networkManager = new NetworkManager(wsAddress, Convert.ToInt32(wsPort), Convert.ToInt32(wsTimeOut));
                    _networkManager.Start();

                    LabelServerStatus.Text = Resources.ServerStatusStart;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ButtonStopClick(object sender, EventArgs e)
        {
            _networkManager.Stop();
            LabelServerStatus.Text = Resources.ServerStatusStop;

            ButtonStart.Enabled = true;
            ButtonStop.Enabled = false;

            TextBoxAddress.Enabled = true;
            TextBoxPort.Enabled = true;
            TextBoxTimeOut.Enabled = true;
            TextBoxDataBase.Enabled = true;
        }

        #endregion
    }
}
