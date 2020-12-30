// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Dal
{
	using Rythm.Server.Dal.Dto;
	using Rythm.Server.Dal.Interfaces;
	using System.Collections.Generic;
	using System.Linq;

	public class MessageRepository : IRepository<MessageDto>
	{
		#region Methods

		public IEnumerable<MessageDto> GetList()
		{
			List<MessageDto> msgList;
			using (var context = new DataBaseContext())
			{
				msgList = context.MsgList.ToList();
			}

			return msgList;
		}

		public MessageDto GetElement(string id)
		{
			using (var context = new DataBaseContext())
			{
				return context.MsgList.Find(id);
			}
		}

		public void Create(MessageDto item)
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

		public void Modify(MessageDto item)
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
