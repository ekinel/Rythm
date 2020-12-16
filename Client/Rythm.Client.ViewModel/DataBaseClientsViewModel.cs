// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Client.ViewModel
{
	public class DataBaseClientsViewModel
	{
		#region Properties

		public string Login { get; }

		#endregion

		#region Constructors

		public DataBaseClientsViewModel(string login)
		{
			Login = login;
		}

		#endregion
	}
}
