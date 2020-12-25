// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Dal
{
	using System.Collections.Generic;
	using System.Linq;

	public class MessageRepository : IRepository<NewMessageDataBase>
	{
		#region Methods

		public IEnumerable<NewMessageDataBase> GetList()
		{
			List<NewMessageDataBase> msgList;
			using (var context = new DataBaseContext())
			{
				msgList = context.MsgList.ToList();
			}

			return msgList;
		}

		public NewMessageDataBase GetElement(string id)
		{
			using (var context = new DataBaseContext())
			{
				return context.MsgList.Find(id);
			}
		}

		public void Create(NewMessageDataBase item)
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

		public void Modify(NewMessageDataBase item)
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
