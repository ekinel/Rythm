// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Dal
{
	using System.ComponentModel.DataAnnotations;

	public class NewEventDataBase
	{
		#region Properties

		[Key]
		public string Date { get; set; }

		public string EventType { get; set; }
		public string Message { get; set; }

		#endregion
	}
}
