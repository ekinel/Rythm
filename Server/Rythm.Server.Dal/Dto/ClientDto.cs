// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Dal.Dto
{
	using System.ComponentModel.DataAnnotations;

	public class ClientDto
	{
		#region Properties

		[Key]
		public string Login { get; set; }

		#endregion
	}
}
