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
		private const string WS_DATABASE_CONNECTION_STRING = "(localdb)/MSSQLLocalDB;Initial Catalog=MessageDb;Integrated Security=True;";

		#endregion

		#region Fields

		private NetworkWrapper _networkWrapper;
		private readonly ServerConfiguration _serverConfiguration;

		#endregion

		#region Constructors

		public RythmServerUI()
		{
			InitializeComponent();
			LabelServerStatus.Text = Resources.ServerStatusStop;

			_serverConfiguration = new ServerConfiguration();
			ServerParameters serverParameters = _serverConfiguration.ReadConfigurationFile();

			TextBoxAddress.Text = serverParameters.Address;
			MaskedTextBoxPort.Text = serverParameters.Port.ToString();
			maskedTextBoxTimeOut.Text = serverParameters.TimeOut.ToString();
			TextBoxDataBase.Text = serverParameters.DataBaseConnectionString;
		}

		#endregion

		#region Methods

		private void ButtonStartClick(object sender, EventArgs e)
		{
			string wsPort = MaskedTextBoxPort.Text;
			string wsAddress = TextBoxAddress.Text;
			string wsTimeOut = maskedTextBoxTimeOut.Text;
			string wsDataBase = TextBoxDataBase.Text;

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

				if (string.IsNullOrEmpty(wsDataBase))
				{
					wsDataBase = WS_DATABASE_CONNECTION_STRING;
					TextBoxDataBase.Text = wsTimeOut;
				}

				_networkWrapper = new NetworkWrapper(wsAddress, Convert.ToInt32(wsPort), Convert.ToInt32(wsTimeOut), wsDataBase);
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
			_serverConfiguration.SaveConfigurationFile(
				TextBoxAddress.Text,
				Convert.ToInt32(MaskedTextBoxPort.Text),
				Convert.ToInt32(maskedTextBoxTimeOut.Text),
				TextBoxDataBase.Text);
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
