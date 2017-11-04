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
            return await GetByApplicantJobOffer(null, jobOfferId);
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByApplicant(int applicantId)
        {
            return await GetByApplicantJobOffer(applicantId, null);
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByApplicantJobOffer(int? applicantId, int? jobOfferId)
        {
            var queryResult =
                await Query.ExecuteQuery(new JobApplicationFilterDto
                {
                    JobOfferId = jobOfferId,
                    ApplicantId = applicantId
                });
            return queryResult.Items;
        }

        public async Task<IEnumerable<JobApplicationDto>> GetByFilter(JobApplicationFilterDto filter)
        {
            var queryResult = await Query.ExecuteQuery(filter);
            return queryResult.Items;
        }
    }
}