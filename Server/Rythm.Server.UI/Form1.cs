// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.UI
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        #region Fields

        private NetworkManager networkManager;

        #endregion

        #region Constructors

        public Form1()
        {
            InitializeComponent();

            MinimumSize = new Size(800, 500);
            MaximumSize = new Size(800, 500);
            ButtonStart.Enabled = true;
            ButtonStop.Enabled = false;
        }

        #endregion

        #region Methods

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            string textPort = TextBoxPort.Text;

            if (!string.IsNullOrEmpty(textPort))
            {
                ButtonStop.Enabled = true;
                ButtonStart.Enabled = false;

                try
                {
                    networkManager = new NetworkManager(textPort);
                    networkManager.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    Console.ReadLine();
                }
            }
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            networkManager.Stop();

            ButtonStart.Enabled = true;
            ButtonStop.Enabled = false;
        }

        #endregion
    }
}
