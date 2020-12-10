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
                    if (string.IsNullOrEmpty(wsAddress))
                    {
                        _networkManager = new NetworkManager(wsPort);
                        _networkManager.Start();
                    }
                    else
                    {
                        _networkManager = new NetworkManager(wsAddress, wsPort);
                        _networkManager.Start();
                    }

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
