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


namespace BusinessLayer.Services.JobApplications
{
    public class JobApplicationService :
        CrudQueryServiceBase<JobApplication, JobApplicationDto, JobApplicationFilterDto>, IJobApplicationService
    {
        public JobApplicationService(IMapper mapper, IRepository<JobApplication> repository,
            QueryObjectBase<JobApplicationDto, JobApplication, JobApplicationFilterDto, IQuery<JobApplication>>
                quoryObject)
            : base(mapper, repository, quoryObject)
        {
        }

        protected override async Task<JobApplication> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId, nameof(JobApplication.Applicant),
                nameof(JobApplication.QuestionAnswers));
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByJobOffer(int jobOfferId)
        {
            var queryResult = await Query.ExecuteQuery(new JobApplicationFilterDto
            {
                JobOfferId = jobOfferId
            });
            return queryResult.Items;
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByApplicant(int applicantId)
        {
            var queryResult = await Query.ExecuteQuery(new JobApplicationFilterDto
            {
                ApplicantId = applicantId
            });
            return queryResult.Items;
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByFilter(JobApplicationFilterDto filter)
        {
            var queryResult = await Query.ExecuteQuery(filter);
            return queryResult.Items;
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

            foreach (var otherApplication in allJobApplications)
            {
                if (!otherApplication.Equals(application))
                    await CloseApplication(applicationId);
            }
            return true;
        }


        private async Task<bool> ChangeApplicationStatus(int applicationId,
            Action<JobApplicationDto> changeFunction)
        {
            JobApplicationDto application = await GetAsync(applicationId, false);
            if (application == null)
            {
                return false;
            }
            changeFunction(application);
            await Update(application);
            return true;
        }
    }
}