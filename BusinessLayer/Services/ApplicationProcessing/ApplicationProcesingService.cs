﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Services.ApplicationProcessing;
using BusinessLayer.Services.JobApplications;
using DAL.Entities;

namespace BusinessLayer.Services.ApplicationProcessing
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
 //           await jobApplicationService.Update(jobApplication);
        }

        public async Task RejectApplication(JobApplicationDto jobApplication)
        {
            jobApplication.JobApplicationStatus = JobApplicationStatus.Rejected;
   //         await jobApplicationService.Update(jobApplication);
        }

        public async Task AcceptApplication(JobApplicationDto jobApplication)
        {
            jobApplication.JobApplicationStatus = JobApplicationStatus.Accepted;
     //       await jobApplicationService.Update(jobApplication);
        }

        public async Task CloseApplication(JobApplicationDto jobApplication)
        {
            jobApplication.JobApplicationStatus = JobApplicationStatus.Closed;
       //     await jobApplicationService.Update(jobApplication);
        }
    }
}
