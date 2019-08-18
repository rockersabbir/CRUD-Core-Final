using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

namespace CoreCRUD.DataAccess
{
    public abstract class Repository<TEntity, TKey, TContext>
        : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TContext : DbContext
    {
        protected TContext _Context;
        protected DbSet<TEntity> _DbSet;

        public Repository(TContext context)
        {
            _Context = context;
            _DbSet = _Context.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            _DbSet.Add(entity);
        }

        public virtual void Remove(TKey id)
        {
            var entityToDelete = _DbSet.Find(id);
            Remove(entityToDelete);
        }

        public virtual void Remove(TEntity entityToDelete)
        {
            if (_Context.Entry(entityToDelete).State == EntityState.Detached) {
                _DbSet.Attach(entityToDelete);
            }
            _DbSet.Remove(entityToDelete);
        }

        public virtual void Remove(Expression<Func<TEntity, bool>> filter)
        {
            _DbSet.RemoveRange(_DbSet.Where(filter));
        }

        public virtual void Edit(TEntity entityToUpdate)
        {
            _DbSet.Attach(entityToUpdate);
            _Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = _DbSet;
            int count = 0;

            if (filter != null) {
                query = query.Where(filter);
                count = query.Count();
            }
            else
                count = query.Count();

            return count;
        }

        public virtual IEnumerable<TEntity> Get(
            out int total, out int totalDisplay,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int pageIndex = 1, int pageSize = 10, bool isTrackingOff = false)
        {
            IQueryable<TEntity> query = _DbSet;
            total = query.Count();
            totalDisplay = query.Count();

            if (filter != null) {
                query = query.Where(filter);
                totalDisplay = query.Count();
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                query = query.Include(includeProperty);
            }

            if (orderBy != null) {
                var result = orderBy(query).Skip((pageIndex - 1) * pageSize).Take(pageSize);

                if (isTrackingOff)
                    return result.AsNoTracking().ToList();
                else
                    return result.ToList();
            }
            else {
                var result = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);

                if (isTrackingOff)
                    return result.AsNoTracking().ToList();
                else
                    return result.ToList();
            }
        }
    }
}
