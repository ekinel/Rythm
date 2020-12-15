// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Dal
{
	using System.Collections.Generic;
	using System.Linq;

	public class ClientRepository : IRepository<NewClientDataBase>
	{
		#region Methods

		public IEnumerable<NewClientDataBase> GetList()
		{
			List<NewClientDataBase> clientsList;
			using (var context = new DataBaseContext())
			{
				clientsList = context.ClientList.ToList();
			}

			return clientsList;
		}

		public NewClientDataBase GetElement(string id)
		{
			using (var context = new DataBaseContext())
			{
				return context.ClientList.Find(id);
			}
		}

		public void Create(NewClientDataBase item)
		{
			using (var context = new DataBaseContext())
			{
				context.ClientList.Add(item);
				context.SaveChanges();
			}
		}

		public void Update(NewClientDataBase item)
		{
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
