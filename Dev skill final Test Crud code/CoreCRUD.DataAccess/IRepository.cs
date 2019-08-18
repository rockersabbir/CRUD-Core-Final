using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CoreCRUD.DataAccess
{
	public interface IRepository<TEntity, TKey>
		where TEntity : class, IEntity<TKey>
	{
		void Add(TEntity entity);

		void Remove(TKey id);

		void Remove(TEntity entityToDelete);

		void Remove(Expression<Func<TEntity, bool>> filter);

		void Edit(TEntity entityToUpdate);

		int GetCount(Expression<Func<TEntity, bool>> filter = null);

		IEnumerable<TEntity> Get(
			out int total, out int totalDisplay,
			Expression<Func<TEntity, bool>> filter = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includeProperties = "", int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false);
	}
}
