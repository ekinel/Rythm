// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Dal
{
	using Rythm.Server.Dal.Dto;
	using Rythm.Server.Dal.Interfaces;
	using System.Collections.Generic;
	using System.Linq;

	public class ClientRepository : IRepository<ClientDto>
	{
		#region Methods

		public IEnumerable<ClientDto> GetList()
		{
			List<ClientDto> clientsList;
			using (var context = new DataBaseContext())
			{
				clientsList = context.ClientList.ToList();
			}

			return clientsList;
		}

		public ClientDto GetElement(string id)
		{
			using (var context = new DataBaseContext())
			{
				return context.ClientList.Find(id);
			}
		}

		public void Create(ClientDto item)
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

		public void Modify(ClientDto item)
		{

		}

		#endregion
	}
}
