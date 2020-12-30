// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.UI
{
	using System;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using Properties;

	using Service;

	public partial class RythmServerUI : Form
	{
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
			var readParameters = _serverConfiguration.ReadConfigurationFile();

			TextBoxAddress.Text = readParameters.Address;
			MaskedTextBoxPort.Text = readParameters.Port.ToString();
			maskedTextBoxTimeOut.Text = readParameters.TimeOut.ToString();
			TextBoxDataBase.Text = readParameters.DataBaseConnectionString;
		}

		#endregion

		#region Methods

		private async void ButtonStartClick(object sender, EventArgs e)
		{

			string wsPort = MaskedTextBoxPort.Text;
			string wsAddress = TextBoxAddress.Text;
			string wsTimeOut = maskedTextBoxTimeOut.Text;
			string wsDataBase = TextBoxDataBase.Text;

			try
			{
				BlockingFields(false);
				await Task.Run(() => InitializationServer(wsAddress, Convert.ToInt32(wsPort), Convert.ToInt32(wsTimeOut), wsDataBase));
				BlockingFields(false);

				LabelServerStatus.Text = Resources.ServerStatusStart;
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				BlockingFields(true);
				_networkWrapper.WriteErrorToDataBase(exception);
			}	
		}

		private void InitializationServer(string wsAddress, int wsPort, int wsTimeOut, string wsDataBase)
		{
			_networkWrapper = new NetworkWrapper(wsAddress, Convert.ToInt32(wsPort), Convert.ToInt32(wsTimeOut), wsDataBase);
			_networkWrapper.Start();
		}


		private void ButtonStopClick(object sender, EventArgs e)
		{
			_networkWrapper.Stop();
			BlockingFields(true);
			_serverConfiguration.SaveConfigurationFile(
				TextBoxAddress.Text,
				Convert.ToInt32(MaskedTextBoxPort.Text),
				Convert.ToInt32(maskedTextBoxTimeOut.Text),
				TextBoxDataBase.Text);

			LabelServerStatus.Text = Resources.ServerStatusStop;
		}

		private void BlockingFields(bool buttonEnabled)
		{
			ButtonStart.Enabled = buttonEnabled;

			TextBoxAddress.Enabled = buttonEnabled;
			MaskedTextBoxPort.Enabled = buttonEnabled;
			maskedTextBoxTimeOut.Enabled = buttonEnabled;
			TextBoxDataBase.Enabled = buttonEnabled;

			if (_networkWrapper == null)
			{
				buttonEnabled = true;
			}

			ButtonStop.Enabled = !buttonEnabled;
		}

		#endregion
	}
}
