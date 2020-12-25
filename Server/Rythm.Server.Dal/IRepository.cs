// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Server.Dal
{
	using System.Collections.Generic;

	public interface IRepository<T>
	{
		#region Methods

		IEnumerable<T> GetList();
		T GetElement(string id);
		void Create(T item);
		void Save();
		void Modify(T item);

		#endregion
	}
}
