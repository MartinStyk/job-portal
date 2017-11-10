using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Facades.Common;
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

        public async Task<bool> AcceptOnlyThisApplication(int applicationId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var result = await jobApplicationService.AcceptOnlyThisApplication(applicationId);
                await uow.Commit();
                return result;
            }
        }


        public async Task<bool> CloseApplication(int applicationId)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                var result = await jobApplicationService.CloseApplication(applicationId);
                await unitOfWork.Commit();
                return result;
            }
        }

        public async Task<bool> RejectApplication(int applicationId)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                var result = await jobApplicationService.RejectApplication(applicationId);
                await unitOfWork.Commit();
                return result;
            }
        }

        public async Task<bool> AcceptApplication(int applicationId)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                var result = await jobApplicationService.AcceptApplication(applicationId);
                await unitOfWork.Commit();
                return result;
            }
        }
    }
}