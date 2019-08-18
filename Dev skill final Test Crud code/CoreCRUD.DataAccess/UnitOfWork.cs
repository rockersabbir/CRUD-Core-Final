using System;

using Microsoft.EntityFrameworkCore;

namespace CoreCRUD.DataAccess
{
    public abstract class UnitOfWork<TContext>
        : IDisposable
        where TContext : DbContext
    {
        protected readonly TContext _DbContext;

        protected UnitOfWork(string connectionString, string migrationAssembly)
        {
            _DbContext = (TContext)Activator.CreateInstance(typeof(TContext), connectionString, migrationAssembly);
        }

        public void Save()
        {
            _DbContext.SaveChanges();
        }

        public void Dispose()
        {
            _DbContext.Dispose();
        }
    }
}
