using System.Threading.Tasks;
using Infrastructure.Entity;

namespace Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        TEntity Get(int id);

        Task<TEntity> GetAsync(int id);

        Task<TEntity> GetAsync(int id, params string[] includes);

        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(int id);
    }
}
