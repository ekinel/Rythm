// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Dal
{
	using System.ComponentModel.DataAnnotations;

	public class ClientDTO
	{
		#region Properties

		[Key]
		public string Login { get; set; }

		#endregion
	}
}
