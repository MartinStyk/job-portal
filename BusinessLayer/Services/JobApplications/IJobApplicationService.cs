using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Services.Common;

namespace BusinessLayer.Services.JobApplications
{
    public interface IJobApplicationService : ICrudService<JobApplicationDto, JobApplicationFilterDto>
    {
        Task<int> Create(JobApplicationCreateDto entityDto);

        /// <summary>
        /// Find all applications for given job offer
        /// </summary>
        /// <param name="jobOfferId">jobOfferId</param>
        /// <returns>Applications for given job offer</returns>
        Task<IEnumerable<JobApplicationDto>> GetByJobOffer(int jobOfferId);

        /// <summary>
        /// Find all applications for given user
        /// </summary>
        /// <param name="applicantId">applicantId</param>
        /// <returns>Applications for given user</returns>
        Task<IEnumerable<JobApplicationDto>> GetByApplicant(int applicantId);

        /// <summary>
        /// Find all applications for given filter
        /// </summary>
        /// <param name="filter">filter</param>
        /// <returns>Applications for given filter</returns>
        Task<IEnumerable<JobApplicationDto>> GetByFilter(JobApplicationFilterDto filter);
    
        /// <summary>
        /// Close application
        /// </summary>
        /// <param name="applicationId">applicationId</param>
        /// <returns>True if update was successfull</returns>
        Task<bool> CloseApplication(int applicationId);

        /// <summary>
        /// Reject application
        /// </summary>
        /// <param name="applicationId">applicationId</param>
        /// <returns>True if update was successfull</returns>
        Task<bool> RejectApplication(int applicationId);
        
        /// <summary>
        /// Accept application
        /// </summary>
        /// <param name="applicationId">applicationId</param>
        /// <returns>True if update was successfull</returns>
        Task<bool> AcceptApplication(int applicationId);

        /// <summary>
        /// Accept application and rejects all others
        /// </summary>
        /// <param name="applicationId">applicationId</param>
        /// <returns>True if update was successfull</returns>
        Task<bool> AcceptOnlyThisApplication(int applicationId);
    }
}