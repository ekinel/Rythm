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
		private const string WS_DATABASE_CONNECTION_STRING = "data source=(localdb)/MSSQLLocalDB;Initial Catalog=MessageDb;Integrated Security=True;";

		private const string ADDRESS_PATTERN = @"^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}$";
		private const int MIN_PORT = 49152;
		private const int MAX_PORT = 65535;

		#endregion

		#region Fields

		private NetworkWrapper _networkWrapper;
		private readonly ServerConfiguration _serverConfiguration;

		private bool _buttonEnabled = true;

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

			(bool checking, string checkMessage) = CheckingParameters(wsAddress, wsPort, wsTimeOut, wsDataBase);

			if (!checking)
			{
				MessageBox.Show(checkMessage);
			}
			else
			{
				try
				{
					BlockingFields();
					_networkWrapper = new NetworkWrapper(wsAddress, Convert.ToInt32(wsPort), Convert.ToInt32(wsTimeOut), wsDataBase);
					_networkWrapper.Start();

					LabelServerStatus.Text = Resources.ServerStatusStart;
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message);
					BlockingFields();
					_networkWrapper.WriteErrorToDataBase(exception);
				}
			}
		}

		private (bool, string) CheckingParameters(string wsAddress, string wsPort, string wsTimeOut, string wsDataBase)
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
					return (false, Resources.NotCorrectIPAddess);
				}

				if (!CorrectPort(wsPort))
				{
					return (false, Resources.NotCorrectPort);
				}
			}

			return (true, string.Empty);
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

			LabelServerStatus.Text = Resources.ServerStatusStop;
		}

		private static bool CorrectAddress(string address)
		{
			var regex = new Regex(ADDRESS_PATTERN);
			Match compare = regex.Match(address);

			return compare.Success;
		}

		private static bool CorrectPort(string port)
		{
			int valuePort = Convert.ToInt32(port);

			return (valuePort > MIN_PORT && valuePort < MAX_PORT);
		}

		private void BlockingFields()
		{
			_buttonEnabled = !_buttonEnabled;

			ButtonStart.Enabled = _buttonEnabled;
			ButtonStop.Enabled = !_buttonEnabled;

			TextBoxAddress.Enabled = _buttonEnabled;
			MaskedTextBoxPort.Enabled = _buttonEnabled;
			maskedTextBoxTimeOut.Enabled = _buttonEnabled;
			TextBoxDataBase.Enabled = _buttonEnabled;
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
