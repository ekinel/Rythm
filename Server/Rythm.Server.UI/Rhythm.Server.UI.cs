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
		private const string WS_ADDRESS = "127.0.0.1";

		#endregion

		#region Fields

		private NetworkWrapper _networkWrapper;

		#endregion

		#region Constructors

		public RythmServerUI()
		{
			InitializeComponent();
			LabelServerStatus.Text = Resources.ServerStatusStop;
			maskedTextBoxTimeOut.Text = WS_TIMEOUT.ToString();
			TextBoxAddress.Text = WS_ADDRESS;
			MaskedTextBoxPort.Text = WS_PORT.ToString();
		}

		#endregion

		#region Methods

		private void ButtonStartClick(object sender, EventArgs e)
		{
			string wsPort = MaskedTextBoxPort.Text;
			string wsAddress = TextBoxAddress.Text;
			string wsTimeOut = maskedTextBoxTimeOut.Text;

			ButtonStop.Enabled = true;
			ButtonStart.Enabled = false;

			MaskedTextBoxPort.Enabled = false;
			TextBoxAddress.Enabled = false;
			maskedTextBoxTimeOut.Enabled = false;
			TextBoxDataBase.Enabled = false;

			try
			{
				if (string.IsNullOrEmpty(wsPort))
				{
					wsPort = WS_PORT.ToString();
					MaskedTextBoxPort.Text = wsPort;
				}

				if (string.IsNullOrEmpty(wsTimeOut))
				{
					wsTimeOut = WS_TIMEOUT.ToString();
					maskedTextBoxTimeOut.Text = wsTimeOut;
				}

				_networkWrapper = new NetworkWrapper(wsAddress, Convert.ToInt32(wsPort), Convert.ToInt32(wsTimeOut));
				_networkWrapper.Start();

				LabelServerStatus.Text = Resources.ServerStatusStart;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				BlockingFields();
			}
		}

		private void ButtonStopClick(object sender, EventArgs e)
		{
			_networkWrapper.Stop();
			BlockingFields();
		}

		private void BlockingFields()
		{
			LabelServerStatus.Text = Resources.ServerStatusStop;

			ButtonStart.Enabled = true;
			ButtonStop.Enabled = false;

			TextBoxAddress.Enabled = true;
			MaskedTextBoxPort.Enabled = true;
			maskedTextBoxTimeOut.Enabled = true;
			TextBoxDataBase.Enabled = true;
		}

		#endregion
	}
}
