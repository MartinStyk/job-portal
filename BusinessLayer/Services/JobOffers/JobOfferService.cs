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
using Infrastructure.Repository;

namespace BusinessLayer.Services.JobOffers
{
    public class JobOfferService : CrudQueryServiceBase<JobOffer, JobOfferDto, JobOfferFilterDto>, IJobOfferService
    {
        private readonly JobOfferRepository jobOfferRepository;
        private readonly IRepository<SkillTag> skillRepository;


        public JobOfferService(IMapper mapper, JobOfferRepository repository,
            QueryObjectBase<JobOfferDto, JobOffer, JobOfferFilterDto, IQuery<JobOffer>> quoryObject, IRepository<SkillTag> skillRepository)
            : base(mapper, repository, quoryObject)
        {
            jobOfferRepository = repository;
            this.skillRepository = skillRepository;
        }

        public override int Create(JobOfferDto entityDto)
        {
            throw new NotImplementedException();
        }

        public async Task Create(JobOfferCreateDto jobOfferCreate)
        {
            JobOffer job = Mapper.Map<JobOffer>(jobOfferCreate);
            job.Skills = new List<SkillTag>();
            job.Questions = new List<Question>();

            foreach (var skillId in jobOfferCreate.SkillsIds)
            {
                var skill = await skillRepository.GetAsync(skillId);
                job.Skills.Add(skill);
                //skillRepository.Update(skill); //just to attach it and not create it twice if exists
            }

            foreach (var questionText in jobOfferCreate.QuestionTexts)
            {
                job.Questions.Add(new Question {Text = questionText});
            }

            Repository.Create(job);
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