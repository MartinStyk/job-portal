using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;

namespace BusinessLayer.Services.ApplicationProcessing
{
    public interface IApplicationProcessingService
    {
        Task OpenApplication(JobApplicationDto jobApplication);

        Task RejectApplication(JobApplicationDto jobApplication);

        Task AcceptApplication(JobApplicationDto jobApplication);

        Task CloseApplication(JobApplicationDto jobApplication);

    }
}
