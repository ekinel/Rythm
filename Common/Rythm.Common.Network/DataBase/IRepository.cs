// ---------------------------------------------------------------------------------------------------------------------------------------------------
// Copyright ElcomPlus LLC. All rights reserved.
// ---------------------------------------------------------------------------------------------------------------------------------------------------

namespace Rythm.Common.Network.DataBase
{
	using System;
	using System.Collections.Generic;

	interface IRepository<T>
	{
		IEnumerable<T> GetList();
		T GetElement(string id);
		void Create(T item);
		void Update(T item);
		void Save();
	}
}
