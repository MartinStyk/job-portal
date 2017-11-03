using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using Infrastructure.EntityFramework.Repository;
using Infrastructure.UnitOfWork;

namespace DAL.Repository
{
    public class JobOfferRepository : EntityFrameworkRepository<JobOffer>
    {
        public JobOfferRepository(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        public async Task<IEnumerable<JobOffer>> GetAsyncBySkills(int[] skillsIds)
        {
            throw new NotImplementedException();
        }
    }
}