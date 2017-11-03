using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Entities;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using Infrastructure.Query;
using Infrastructure.Repository;


namespace BusinessLayer.Services.JobOffers
{
    public class JobOfferService : CrudQueryServiceBase<JobOffer, JobOfferDto, JobOfferFilterDto>, IJobOfferService
    {
        public JobOfferService(IMapper mapper, IRepository<JobOffer> repository,
            QueryObjectBase<JobOfferDto, JobOffer, JobOfferFilterDto, IQuery<JobOffer>> quoryObject)
            : base(mapper, repository, quoryObject)
        {
        }

        protected override async Task<JobOffer> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId, nameof(JobOffer.Employer), nameof(JobOffer.Skills), nameof(JobOffer.Questions));
        }
        
        public async Task<IEnumerable<JobOfferDto>> GetByEmployer(int employerId)
        {
            var queryResult = await Query.ExecuteQuery(new JobOfferFilterDto { EmployerId =  employerId});
            return queryResult.Items;
        }

        public async Task<IEnumerable<JobOfferDto>> GetBySkills(int[] skillIds)
        {
            var queryResult = await Query.ExecuteQuery(new JobOfferFilterDto { SkillIds = skillIds });
            return queryResult.Items;
        }

    }
}