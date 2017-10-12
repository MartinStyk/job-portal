using DAL.Entities;
using Infrastructure.EntityFramework.Repository;
using Infrastructure.UnitOfWork;

namespace DAL.Repository
{
    public class ApplicantRepository : EntityFrameworkRepository<Applicant, int>
    {
        public ApplicantRepository(IUnitOfWorkProvider provider) : base(provider)
        {
        }
    }
}