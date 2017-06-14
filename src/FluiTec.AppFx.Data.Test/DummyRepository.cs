using System;
using System.Collections.Generic;
using FluiTec.AppFx.Data.Test.Fixtures;

namespace FluiTec.AppFx.Data.Test
{
	public class DummyRepository : IDummyRepository
	{
		public DummyEntity Get(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<DummyEntity> GetAll()
		{
			throw new NotImplementedException();
		}

		public DummyEntity Add(DummyEntity entity)
		{
			throw new NotImplementedException();
		}

		public void AddRange(IEnumerable<DummyEntity> entities)
		{
			throw new NotImplementedException();
		}

		public DummyEntity Update(DummyEntity entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public void Delete(DummyEntity entity)
		{
			throw new NotImplementedException();
		}
	}
}