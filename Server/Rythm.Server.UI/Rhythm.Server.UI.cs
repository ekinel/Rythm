// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.UI
{
    using System;
    using System.Windows.Forms;
    using System.Resources;

    using Service;

    public partial class RhythmServerUI : Form
    {
        #region Fields

        private NetworkManager networkManager;

        #endregion

        #region Constructors

        public RhythmServerUI()
        {
            InitializeComponent();
            LabelServerStatus.Text = Properties.Resources.ServerStatusStop;
        }

        #endregion

        #region Methods

        private void ButtonStartClick(object sender, EventArgs e)
        {
            var wsPort = TextBoxPort.Text;
            var wsAddress = TextBoxAddress.Text;

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
                        networkManager = new NetworkManager(wsPort);
                    }
                    else
                    {
                        networkManager = new NetworkManager(wsAddress, wsPort);
                    }

                    LabelServerStatus.Text = Properties.Resources.ServerStatusStart;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ButtonStopClick(object sender, EventArgs e)
        {
            networkManager.Stop();
            LabelServerStatus.Text = Properties.Resources.ServerStatusStop;

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
