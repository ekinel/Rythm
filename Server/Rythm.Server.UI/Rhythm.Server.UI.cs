// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.UI
{
	using System;
	using System.Text.RegularExpressions;
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

		private const string ADDRESS_PATTERN = @"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}$";

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

			if (FillingFields())
			{
				return;
			}

			TextBoxAddress.Text = WS_ADDRESS;
			MaskedTextBoxPort.Text = WS_PORT.ToString();
			maskedTextBoxTimeOut.Text = WS_TIMEOUT.ToString();
			TextBoxDataBase.Text = WS_DATABASE_CONNECTION_STRING;
		}

		#endregion

		#region Methods

		private void ButtonStartClick(object sender, EventArgs e)
		{
			string wsPort = MaskedTextBoxPort.Text;
			string wsAddress = TextBoxAddress.Text;
			string wsTimeOut = maskedTextBoxTimeOut.Text;
			string wsDataBase = TextBoxDataBase.Text;

			if (!CheckingParameters(wsAddress, wsPort, wsTimeOut, wsDataBase))
			{
				MessageBox.Show("IP-address isn't corrects");
			}
			else
			{
				try
				{
					ButtonStop.Enabled = true;
					ButtonStart.Enabled = false;

					MaskedTextBoxPort.Enabled = false;
					TextBoxAddress.Enabled = false;
					maskedTextBoxTimeOut.Enabled = false;
					TextBoxDataBase.Enabled = false;

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
		}

		private bool CheckingParameters(string wsAddress, string wsPort, string wsTimeOut, string wsDataBase)
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

			if (string.IsNullOrEmpty(wsAddress))
			{
				wsAddress = WS_ADDRESS;
				TextBoxAddress.Text = wsAddress;
			}
			else
			{
				if (!CorrectAddress(wsAddress))
				{
					return false;
				}
			}

			return true;
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

		private bool CorrectAddress(string address)
		{
			var regex = new Regex(ADDRESS_PATTERN);
			Match compare = regex.Match(address);

			return compare.Success;
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

		private bool FillingFields()
		{
			ServerParameters serverParameters = _serverConfiguration.ReadConfigurationFile();
			if (string.IsNullOrEmpty(serverParameters.Address))
			{
				return false;
			}

			TextBoxAddress.Text = serverParameters.Address;
			MaskedTextBoxPort.Text = serverParameters.Port.ToString();
			maskedTextBoxTimeOut.Text = serverParameters.TimeOut.ToString();
			TextBoxDataBase.Text = serverParameters.DataBaseConnectionString;

			return true;
		}

		#endregion
	}
}
