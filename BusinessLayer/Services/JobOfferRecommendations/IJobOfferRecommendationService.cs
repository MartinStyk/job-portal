using System;
using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;


namespace BusinessLayer.Services.JobOfferRecommendations
{
    public interface IJobOfferRecommendationService
    {
        /// <summary>
        /// Find best jobs for given user, based on user skills
        /// </summary>
        /// <param name="user">user</param>
        /// <param name="jobOffers">jobOffers</param>
        /// <param name="numberOfResults">numberOfResults</param>
        /// <returns>Best jobs for given user</returns>
        IList<JobOfferDto> GetBestOffersForUser(UserDto user, IEnumerable<JobOfferDto> jobOffers, int numberOfResults);

        /// <summary>
        /// Returns random set of jobs
        /// </summary>
        /// <param name="jobOffers">jobOffers</param>
        /// <param name="numberOfResults">numberOfResults</param>
        /// <returns>Random set of job offers</returns>
        IList<JobOfferDto> GetRandomOffers(IEnumerable<JobOfferDto> jobOffers, int numberOfResults);
    }
}