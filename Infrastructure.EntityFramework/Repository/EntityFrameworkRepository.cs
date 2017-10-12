using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Entity;
using Infrastructure.EntityFramework.UnitOfWork;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;

namespace Infrastructure.EntityFramework.Repository
{
    public class EntityFrameworkRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>, new()
    {
        private readonly IUnitOfWorkProvider provider;

        /// <summary>
        /// Gets the <see cref="DbContext"/>.
        /// </summary>
        protected DbContext Context => ((EntityFrameworkUnitOfWork) provider.GetInstance()).Context;

        public EntityFrameworkRepository(IUnitOfWorkProvider provider)
        {
            this.provider = provider;
        }


        public void Create(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public TEntity Get(TKey id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public async Task<TEntity> GetAsync(TKey id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetAsync(TKey id, params string[] includes)
        {
            DbQuery<TEntity> ctx = Context.Set<TEntity>();
            foreach (var include in includes)
            {
                ctx = ctx.Include(include);
            }
            return await ctx.SingleOrDefaultAsync(entity => entity.Id.Equals(id));
        }

        public void Update(TEntity entity)
        {
            var foundEntity = Context.Set<TEntity>().Find(entity.Id);
            Context.Entry(foundEntity).CurrentValues.SetValues(entity);
        }

        public void Delete(TKey id)
        {
            var entity = Context.Set<TEntity>().Find(id);
            if (entity != null)
            {
                Context.Set<TEntity>().Remove(entity);
            }
        }
    }
}