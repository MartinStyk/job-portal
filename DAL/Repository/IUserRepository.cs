
using DAL.Entities;
using Infrastructure.Repository;

namespace DAL.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string mail);
    }
}
