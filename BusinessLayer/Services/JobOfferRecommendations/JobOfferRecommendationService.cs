using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Entities;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using Infrastructure.Query;
using Infrastructure.Repository;


namespace BusinessLayer.Services.JobOfferRecommendations
{
    public class JobOfferRecommendationService : IJobOfferRecommendationService
    {
        public JobOfferRecommendationService()
        {
        }

        public IList<JobOfferDto> GetBestOffersForUser(UserDto user, IEnumerable<JobOfferDto> jobOffers, int numberOfResult)
        {
            List<SkillTagDto> userSkills = user.Skills;
            Dictionary<JobOfferDto, int> scoreBoard = new Dictionary<JobOfferDto, int>();
            foreach (var jobOffer in jobOffers)
            {
                foreach (var skill in jobOffer.Skills)
                {
                    if (userSkills.Contains(skill))
                    {
                        if (scoreBoard.ContainsKey(jobOffer))
                        {
                            scoreBoard[jobOffer] = scoreBoard[jobOffer] + 1;
                        }
                        else
                        {
                            scoreBoard.Add(jobOffer, 1);
                        }
                    }
                }
            }

            var scoreList = scoreBoard.ToList();
            scoreList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

            IList<JobOfferDto> results = new List<JobOfferDto>(numberOfResult);
            for (int i = 0; i < Math.Min(numberOfResult, scoreList.Count); i++)
            {
                results.Add(scoreList[i].Key);
            }

            return results;

        }
    }
}