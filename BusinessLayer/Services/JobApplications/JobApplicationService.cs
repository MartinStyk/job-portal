using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.WebSockets;
using AutoMapper;
using DAL.Entities;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using DAL.Repository;
using Infrastructure.Query;
using Infrastructure.Repository;


namespace BusinessLayer.Services.JobApplications
{
    public class JobApplicationService :
        CrudQueryServiceBase<JobApplication, JobApplicationDto, JobApplicationFilterDto>, IJobApplicationService
    {
        private readonly IRepository<JobOffer> jobOfferRepository;
        private readonly IUserRepository userRepository;


        public JobApplicationService(IMapper mapper, IRepository<JobApplication> repository,
            QueryObjectBase<JobApplicationDto, JobApplication, JobApplicationFilterDto, IQuery<JobApplication>>
                quoryObject, IRepository<JobOffer> jobOfferRepository, IUserRepository userRepository)
            : base(mapper, repository, quoryObject)
        {
            this.jobOfferRepository = jobOfferRepository;
            this.userRepository = userRepository;
        }

        public override int Create(JobApplicationDto entityDto)
        {
            throw new NotImplementedException("Use JobApplicationCreateDto");
        }

        public async Task<int> Create(JobApplicationCreateDto entityDto)
        {
            var applicationEntity = Mapper.Map<JobApplication>(entityDto);

            var jobOffer = await jobOfferRepository.GetAsync(entityDto.JobOfferId);
            var registeredUser = userRepository.GetByEmail(entityDto.Applicant.Email);

            if (registeredUser != null)
            {
                applicationEntity.Applicant = registeredUser;
                applicationEntity.ApplicantId = registeredUser.Id;
            }

            applicationEntity.QuestionAnswers = new List<QuestionAnswer>();
            if (entityDto.QuestionAnswers != null)
                for (int i = 0; i < entityDto.QuestionAnswers.Count; i++)
                {
                    var answerEntity = new QuestionAnswer()
                    {
                        Application = applicationEntity,
                        Text = entityDto.QuestionAnswers[i].Text,
                        Question = jobOffer.Questions[i]
                    };

                    applicationEntity.QuestionAnswers.Add(answerEntity);
                }

            Repository.Create(applicationEntity);
            return applicationEntity.Id;
        }


        protected override async Task<JobApplication> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId, nameof(JobApplication.Applicant),
                nameof(JobApplication.QuestionAnswers));
        }

        public async Task<QueryResultDto<JobApplicationDto, JobApplicationFilterDto>> GetByJobOffer(int jobOfferId)
        {
            var queryResult = await Query.ExecuteQuery(new JobApplicationFilterDto
            {
                JobOfferId = jobOfferId
            });
            return queryResult;
        }

        public async Task<QueryResultDto<JobApplicationDto, JobApplicationFilterDto>> GetByApplicant(int applicantId)
        {
            var queryResult = await Query.ExecuteQuery(new JobApplicationFilterDto
            {
                ApplicantId = applicantId
            });
            return queryResult;
        }

        public async Task<QueryResultDto<JobApplicationDto, JobApplicationFilterDto>> GetByFilter(
            JobApplicationFilterDto filter)
        {
            var queryResult = await Query.ExecuteQuery(filter);
            return queryResult;
        }

        public async Task<bool> CloseApplication(int applicationId)
        {
            return await ChangeApplicationStatus(applicationId,
                job => job.JobApplicationStatus = JobApplicationStatus.Closed);
        }

        public async Task<bool> RejectApplication(int applicationId)
        {
            return await ChangeApplicationStatus(applicationId,
                job => job.JobApplicationStatus = JobApplicationStatus.Rejected);
        }

        public async Task<bool> AcceptApplication(int applicationId)
        {
            return await ChangeApplicationStatus(applicationId,
                job => job.JobApplicationStatus = JobApplicationStatus.Accepted);
        }

        public async Task<bool> AcceptOnlyThisApplication(int applicationId)
        {
            var application = await GetAsync(applicationId);
            if (application == null)
            {
                return false;
            }
            await AcceptApplication(applicationId);
            var allJobApplications = await GetByJobOffer(application.JobOfferId);
            foreach (var otherApplication in allJobApplications.Items)
            {
                if (otherApplication.Id != application.Id)
                    await CloseApplication(applicationId);
            }
            return true;
        }

        private async Task<bool> ChangeApplicationStatus(int applicationId,
            Action<JobApplication> changeFunction)
        {
            var application = await Repository.GetAsync(applicationId);
            if (application == null)
            {
                return false;
            }
            changeFunction(application);
            Repository.Update(application);
            return true;
        }
    }
}