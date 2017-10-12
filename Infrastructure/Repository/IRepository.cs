using System.Threading.Tasks;
using Infrastructure.Entity;

namespace Infrastructure.Repository
{
    public interface IRepository<TEntity, in TKey> where TEntity : class, IEntity<TKey>, new()
    {
        TEntity Get(TKey id);

        Task<TEntity> GetAsync(TKey id);

        Task<TEntity> GetAsync(TKey id, params string[] includes);

        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(TKey id);
    }
}
