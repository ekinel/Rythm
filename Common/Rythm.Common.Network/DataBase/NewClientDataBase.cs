// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.DataBase
{
	using System.ComponentModel.DataAnnotations;

	public class NewClientDataBase
	{
		#region Properties

		[Key]
		public string Login { get; set; }

		#endregion
	}
}
