// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.DataBase
{
	public class NewClientDataBase
	{
		#region Properties

		public string Login { get; set; }

		#endregion

		#region Constructors

		public NewClientDataBase(string login)
		{
			Login = login;
		}

		#endregion
	}
}
