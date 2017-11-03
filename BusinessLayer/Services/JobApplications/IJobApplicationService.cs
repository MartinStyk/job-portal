using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Services.Common;
using DAL.Entities;

namespace BusinessLayer.Services.JobApplications
{
    public interface IJobApplicationService : ICrudService<JobApplicationDto, JobApplicationFilterDto>
    {
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
        /// Find all applications for given user and job
        /// </summary>
        /// <param name="applicantId">applicantId</param>
        /// <param name="jobOfferId">jobOfferId</param>
        /// <returns>Applications for given user and job</returns>
        Task<IEnumerable<JobApplicationDto>> GetByApplicantJobOffer(int? applicantId, int? jobOfferId);
    }
}