// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Dal
{
	using Rythm.Server.Dal.Interfaces;
	using System.Collections.Generic;
	using System.Linq;

	public class MessageRepository : IRepository<MessageDTO>
	{
		#region Methods

		public IEnumerable<MessageDTO> GetList()
		{
			List<MessageDTO> msgList;
			using (var context = new DataBaseContext())
			{
				msgList = context.MsgList.ToList();
			}

			return msgList;
		}

		public MessageDTO GetElement(string id)
		{
			using (var context = new DataBaseContext())
			{
				return context.MsgList.Find(id);
			}
		}

		public void Create(MessageDTO item)
		{
			using (var context = new DataBaseContext())
			{
				context.MsgList.Add(item);
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

		public void Modify(MessageDTO item)
		{
			using (var context = new DataBaseContext())
			{
				var element = context.MsgList.FirstOrDefault(message => message.ID == item.ID);

				if(element != null)
				{
					element.MsgStatus = "ClientOk";
					context.SaveChanges();
				}
			}
		}

		#endregion
	}
}
