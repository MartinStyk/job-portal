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
        /// Find all offers for given name
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>Job offers for given name</returns>
        Task<IEnumerable<JobOfferDto>> GetByName(string name);

        /// <summary>
        /// Find all offers for given skills
        /// </summary>
        /// <param name="skill">skill</param>
        /// <returns>Job offers for given set of skills</returns>
        Task<IList<JobOfferDto>> GetBySkills(SkillTagDto skill);


        /// <summary>
        /// Find all offers matching filter criteria
        /// </summary>
        /// <param name="filter">filter</param>
        /// <returns>Job offers matching filter criteria</returns>
        Task<IEnumerable<JobOfferDto>> GetFiltered(JobOfferFilterDto filter);
    }
}