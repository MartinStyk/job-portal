using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;
using Infrastructure.Repository;

namespace DAL.Repository
{
    public interface IJobOfferRepository : IRepository<JobOffer>
    {
        Task<List<JobOffer>> GetBySkill(SkillTag skillTag);
    }
}
