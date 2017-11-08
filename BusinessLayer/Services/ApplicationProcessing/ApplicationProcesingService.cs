using System;
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
    /// <summary>
    /// This service is doing very basic logic right now.
    /// It servers as a  
    /// </summary>
    public class ApplicationProcesingService : IApplicationProcessingService
    {
        public async Task OpenApplication(JobApplicationDto jobApplication)
        {
            jobApplication.JobApplicationStatus = JobApplicationStatus.Open;
        }

        public async Task RejectApplication(JobApplicationDto jobApplication)
        {
            jobApplication.JobApplicationStatus = JobApplicationStatus.Rejected;
        }

        public async Task AcceptApplication(JobApplicationDto jobApplication)
        {
            jobApplication.JobApplicationStatus = JobApplicationStatus.Accepted;
        }

        public async Task CloseApplication(JobApplicationDto jobApplication)
        {
            jobApplication.JobApplicationStatus = JobApplicationStatus.Closed;
        }
    }
}
