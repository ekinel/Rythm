// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Dal
{
	using System.Data.Entity;
	using System.Data.Entity.ModelConfiguration.Conventions;

	public class DataBaseContext : DbContext
	{
		#region Properties

		public DbSet<NewClientDataBase> ClientList { get; set; }
		public DbSet<NewMessageDataBase> MsgList { get; set; }
		public DbSet<NewEventDataBase> EventList { get; set; }

		#endregion

		#region Constructors

		public DataBaseContext()
			: base("DBConnection")
		{
			//Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataBaseContext, Migrations.Configuration>());
		}

		#endregion

		#region Methods

		//protected override void OnModelCreating(DbModelBuilder modelBuilder)
		//{
		//	base.OnModelCreating(modelBuilder);
		//}
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			//base.OnModelCreating(modelBuilder);
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}

		#endregion
	}
}
