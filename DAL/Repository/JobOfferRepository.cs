using DAL.Entities;
using Infrastructure.EntityFramework.Repository;
using Infrastructure.UnitOfWork;

namespace DAL.Repository
{
    public class JobOfferRepository : EntityFrameworkRepository<JobOffer, int>
    {
        public JobOfferRepository(IUnitOfWorkProvider provider) : base(provider)
        {
        }
    }
}