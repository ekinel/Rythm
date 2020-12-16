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

		private NetworkWrapper _networkWrapper;

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
					TextBoxPort.Text = wsPort;
				}

				if (string.IsNullOrEmpty(wsTimeOut))
				{
					wsTimeOut = WS_TIMEOUT.ToString();
					TextBoxTimeOut.Text = wsTimeOut;
				}

				_networkWrapper = new NetworkWrapper(wsAddress, Convert.ToInt32(wsPort), Convert.ToInt32(wsTimeOut));
				_networkWrapper.Start();

				LabelServerStatus.Text = Resources.ServerStatusStart;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void ButtonStopClick(object sender, EventArgs e)
		{
			_networkWrapper.Stop();
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
