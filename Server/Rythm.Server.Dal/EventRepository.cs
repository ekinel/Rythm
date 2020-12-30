// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Dal
{
	using Rythm.Server.Dal.Dto;
	using Rythm.Server.Dal.Interfaces;
	using System.Collections.Generic;
	using System.Linq;

	public class EventRepository : IRepository<EventDto>
	{
		#region Methods

		public IEnumerable<EventDto> GetList()
		{
			List<EventDto> eventList;
			using (var context = new DataBaseContext())
			{
				eventList = context.EventList.ToList();
			}

			return eventList;
		}

		public EventDto GetElement(string id)
		{
			using (var context = new DataBaseContext())
			{
				return context.EventList.Find(id);
			}
		}

		public void Create(EventDto item)
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

		public void Modify(EventDto item)
		{

		}

		#endregion
	}
}
