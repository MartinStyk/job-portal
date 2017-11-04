using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.ApplicationProcessing;
using BusinessLayer.Services.JobApplications;
using DAL.Entities;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades
{
    public class JobApplicationFacade : FacadeBase
    {
        private readonly IJobApplicationService jobApplicationService;


        public JobApplicationFacade(IUnitOfWorkProvider unitOfWorkProvider,
            IJobApplicationService jobApplicationService)
            : base(unitOfWorkProvider)
        {
            this.jobApplicationService = jobApplicationService;
        }


        public async Task CreateApplication(JobApplicationDto jobApplication)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                jobApplication.JobApplicationStatus = JobApplicationStatus.Open;
                jobApplicationService.Create(jobApplication);
                await uow.Commit();
            }
        }

        public async Task<bool> CloseApplication(int applicationId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                JobApplicationDto application =await jobApplicationService.GetAsync(applicationId, false);

                if (application == null)
                {
                    return false;
                }
                application.JobApplicationStatus = JobApplicationStatus.Closed;
                await jobApplicationService.Update(application);
                await uow.Commit();
                return true;
            }
        }

        public async Task<JobApplicationDto> GetApplication(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobApplicationService.GetAsync(id);
            }
        }

        public async Task<IEnumerable<JobApplicationDto>> GetAllApplications()
        {
            using (UnitOfWorkProvider.Create())
            {
                return (await jobApplicationService.ListAllAsync()).Items;
            }
        }


        public async Task<IEnumerable<JobApplicationDto>> GetApplicationsForFilter(JobApplicationFilterDto filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobApplicationService.GetByFilter(filter);
            }
        }
    }
}