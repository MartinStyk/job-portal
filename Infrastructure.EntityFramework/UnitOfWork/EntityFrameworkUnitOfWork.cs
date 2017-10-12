using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.UnitOfWork;

namespace Infrastructure.EntityFramework.UnitOfWork
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; }

        public EntityFrameworkUnitOfWork(Func<DbContext> dbContextFactory)
        {
            Context = dbContextFactory?.Invoke() ?? throw new ArgumentException("Db context factory is null!");
        }

        public async Task Commit()
        {
            await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }

}