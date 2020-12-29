// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Dal
{
	using Rythm.Server.Dal.Interfaces;
	using System.Collections.Generic;
	using System.Linq;

	public class EventRepository : IRepository<EventDTO>
	{
		#region Methods

		public IEnumerable<EventDTO> GetList()
		{
			List<EventDTO> eventList;
			using (var context = new DataBaseContext())
			{
				eventList = context.EventList.ToList();
			}

			return eventList;
		}

		public EventDTO GetElement(string id)
		{
			using (var context = new DataBaseContext())
			{
				return context.EventList.Find(id);
			}
		}

		public void Create(EventDTO item)
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

		public void Modify(EventDTO item)
		{

		}

		#endregion
	}
}
