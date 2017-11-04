using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public async Task<List<JobOffer>> GetBySkill(SkillTag skillTag)
        {
            var skill = await Context.Set<SkillTag>().FindAsync(skillTag.Id);
            if (skill == null)
                return null;
            return skill.JobOffers;
        }
    }
}