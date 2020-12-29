// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Dal
{
	using System.Collections.Generic;
	using System.Linq;

	public class ClientRepository : IRepository<ClientDTO>
	{
		#region Methods

		public IEnumerable<ClientDTO> GetList()
		{
			List<ClientDTO> clientsList;
			using (var context = new DataBaseContext())
			{
				clientsList = context.ClientList.ToList();
			}

			return clientsList;
		}

		public ClientDTO GetElement(string id)
		{
			using (var context = new DataBaseContext())
			{
				return context.ClientList.Find(id);
			}
		}

		public void Create(ClientDTO item)
		{
			using (var context = new DataBaseContext())
			{
				context.ClientList.Add(item);
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

		public void Modify(ClientDTO item)
		{

		}

		#endregion
	}
}
