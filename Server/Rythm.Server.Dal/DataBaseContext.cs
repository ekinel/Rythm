﻿// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Dal
{
	using Rythm.Server.Dal.Dto;
	using System.Data.Entity;
	using System.Data.Entity.ModelConfiguration.Conventions;

	public class DataBaseContext : DbContext
	{
		#region Properties

		public DbSet<ClientDto> ClientList { get; set; }
		public DbSet<MessageDto> MsgList { get; set; }
		public DbSet<EventDto> EventList { get; set; }

		#endregion

		#region Constructors

		public DataBaseContext()
			: base("DBConnection")
		{
		}

		#endregion

		#region Methods

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}

		#endregion
	}
}
