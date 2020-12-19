// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Dal
{
	using System.Collections.Generic;
	using System.Linq;

	public class EventRepository : IRepository<NewEventDataBase>
	{
		#region Methods

		public IEnumerable<NewEventDataBase> GetList()
		{
			List<NewEventDataBase> eventList;
			using (var context = new DataBaseContext())
			{
				eventList = context.EventList.ToList();
			}

			return eventList;
		}

		public NewEventDataBase GetElement(string id)
		{
			using (var context = new DataBaseContext())
			{
				return context.EventList.Find(id);
			}
		}

		public void Create(NewEventDataBase item)
		{
			using (var context = new DataBaseContext())
			{
				context.EventList.Add(item);
				context.SaveChanges();
			}
		}

		public void Save()
		{
			using (var context = new DataBaseContext())
			{
				context.SaveChanges();
			}
		}

		#endregion
	}
}
