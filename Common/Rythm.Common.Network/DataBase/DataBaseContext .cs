// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.DataBase
{
	using System.Data.Entity;

	public class DataBaseContext : DbContext
	{
		#region Properties

		public DbSet<NewClientDataBase> Client { get; set; }

		#endregion

		#region Constructors

		public DataBaseContext()
			: base("DBConnection")
		{
		}

		#endregion
	}
}
