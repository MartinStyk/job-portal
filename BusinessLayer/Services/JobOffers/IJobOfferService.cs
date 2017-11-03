using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Services.Common;
using DAL.Entities;

namespace BusinessLayer.Services.JobOffers
{
    public interface IJobOfferService : ICrudService<JobOfferDto, JobOfferFilterDto>
    {
        /// <summary>
        /// Find all offers for given employer
        /// </summary>
        /// <param name="employerId">employerId</param>
        /// <returns>Job offers for given employer</returns>
        Task<IEnumerable<JobOfferDto>> GetByEmployer(int employerId);

        /// <summary>
        /// Find all offers for given skills
        /// </summary>
        /// <param name="skillIds">skillIds</param>
        /// <returns>Job offers for given set of skills</returns>
        Task<IEnumerable<JobOffer>> GetBySkills(int[] skillIds);
    }
}