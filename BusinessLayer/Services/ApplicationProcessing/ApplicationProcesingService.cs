using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Services.ApplicationProcessing;
using BusinessLayer.Services.JobApplications;
using DAL.Entities;

namespace BusinessLayer.Services
{
    public class ApplicationProcesingService : IApplicationProcessingService
    {
        private readonly IJobApplicationService jobApplicationService;

        public ApplicationProcesingService(JobApplicationService jobApplicationService)
        {
            this.jobApplicationService = jobApplicationService;
        }

        public async Task OpenApplication(JobApplicationDto jobApplication)
        {
            jobApplication.JobApplicationStatus = JobApplicationStatus.Open;
            await jobApplicationService.Update(jobApplication);
        }
    }
}
