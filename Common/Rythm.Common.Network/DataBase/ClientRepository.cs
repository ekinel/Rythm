using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rythm.Common.Network.DataBase
{
	class ClientRepository : IRepository<NewClientDataBase>
	{
		public List<NewClientDataBase> ClientsList = new List<NewClientDataBase>();

		public IEnumerable<NewClientDataBase> GetList()
		{
			return ClientsList;
		}

		public NewClientDataBase GetElement(int id)
		{
			return ClientsList[id];
		}

		public void Create(NewClientDataBase item)
		{
		}

		public void Update(NewClientDataBase item)
		{
		}

		public void Delete(int id)
		{
		}

		public void Save()
		{
		}
	}
}
