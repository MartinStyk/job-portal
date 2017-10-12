using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.UnitOfWork;

namespace Infrastructure.EntityFramework.UnitOfWork
{
    class EntityFrameworkUnitOfWorkProvider : UnitOfWorkProviderBase
    {
        private readonly Func<DbContext> dbContextFactory;

        public EntityFrameworkUnitOfWorkProvider(Func<DbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public override IUnitOfWork Create()
        {
            UowLocalInstance.Value = new EntityFrameworkUnitOfWork(dbContextFactory);
            return UowLocalInstance.Value;
        }

    }
}