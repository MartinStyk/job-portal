using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Entities;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using DAL.Repository;
using Infrastructure.Query;

namespace BusinessLayer.Services.JobOffers
{
    public class JobOfferService : CrudQueryServiceBase<JobOffer, JobOfferDto, JobOfferFilterDto>, IJobOfferService
    {
        private readonly JobOfferRepository jobOfferRepository;
        private readonly SkillRepository skillRepository;


        public JobOfferService(IMapper mapper, JobOfferRepository repository,
            QueryObjectBase<JobOfferDto, JobOffer, JobOfferFilterDto, IQuery<JobOffer>> quoryObject,
            SkillRepository skillRepository)
            : base(mapper, repository, quoryObject)
        {
            jobOfferRepository = repository;
            this.skillRepository = skillRepository;
        }

        public override int Create(JobOfferDto entityDto)
        {
            throw new NotImplementedException();
        }

        public int Create(JobOfferCreateDto jobOfferCreate)
        {
            JobOffer job = Mapper.Map<JobOffer>(jobOfferCreate);
            job.Skills = new List<SkillTag>();

            if (jobOfferCreate.SkillNames != null)
                foreach (var skillName in jobOfferCreate.SkillNames)
                {
                    var skill = skillRepository.GetByName(skillName);
                    job.Skills.Add(skill);
                }

            Repository.Create(job);
            return job.Id;
        }

        public override Task Update(JobOfferDto entityDto)
        {
            throw new NotImplementedException();
        }

        public async Task Update(JobOfferCreateDto jobOfferCreate)
        {

            var entity = await GetWithIncludesAsync(jobOfferCreate.Id);
            JobOffer job = Mapper.Map(jobOfferCreate, entity);
            job.Skills = new List<SkillTag>();

            if (jobOfferCreate.SkillNames != null)
                foreach (var skillName in jobOfferCreate.SkillNames)
                {
                    var skill = skillRepository.GetByName(skillName);
                    job.Skills.Add(skill);
                }

            Repository.Update(job);
        }


        protected override async Task<JobOffer> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId, nameof(JobOffer.Employer), nameof(JobOffer.Skills),
                nameof(JobOffer.Questions));
        }

        public async Task<IEnumerable<JobOfferDto>> GetByEmployer(int employerId)
        {
            var queryResult = await Query.ExecuteQuery(new JobOfferFilterDto {EmployerId = employerId});
            return queryResult.Items;
        }

        public async Task<IEnumerable<JobOfferDto>> GetByName(string name)
        {
            var queryResult = await Query.ExecuteQuery(new JobOfferFilterDto {Name = name});
            return queryResult.Items;
        }

        public async Task<IList<JobOfferDto>> GetBySkills(SkillTagDto skillTagDto)
        {
            SkillTag skillTag = Mapper.Map<SkillTag>(skillTagDto);
            return Mapper.Map<IList<JobOfferDto>>(await (jobOfferRepository.GetBySkill(skillTag)));
        }

        public async Task<IEnumerable<JobOfferDto>> GetFiltered(JobOfferFilterDto filter)
        {
            var queryResult = await Query.ExecuteQuery(filter);
            return queryResult.Items;
        }
    }
}