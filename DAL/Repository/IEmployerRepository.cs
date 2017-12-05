
using DAL.Entities;
using Infrastructure.Repository;

namespace DAL.Repository
{
    public interface IEmployerRepository : IRepository<Employer>
    {
        Employer GetByEmail(string mail);
    }
}
