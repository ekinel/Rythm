// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Dal.Dto
{
	using System;

	public class EventDto
	{
		#region Properties

		public int ID { get; set; }
		public DateTime Date { get; set; }
		public string Message { get; set; }

		#endregion
	}
}
