using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.JobOfferRecommendations;
using BusinessLayer.Services.JobOffers;
using BusinessLayer.Services.Skills;
using BusinessLayer.Services.Users;
using Castle.Core.Internal;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades
{
    public class JobOfferFacade : FacadeBase
    {
        private readonly IJobOfferService jobOfferService;
        private readonly ISkillService skillService;
        private readonly IJobOfferRecommendationService jobOfferRecommendationService;
        private readonly IUserService userService;

        public JobOfferFacade(IUnitOfWorkProvider unitOfWorkProvider, IMapper mapper,
            IJobOfferService jobOfferService,
            ISkillService skillService,
            IJobOfferRecommendationService jobOfferRecommendationService,
            IUserService userService) : base(unitOfWorkProvider)
        {
            this.jobOfferService = jobOfferService;
            this.skillService = skillService;
            this.jobOfferRecommendationService = jobOfferRecommendationService;
            this.userService = userService;
        }

        #region Search and listings

        public async Task<JobOfferDto> GetOffer(int offerId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobOfferService.GetAsync(offerId);
            }
        }

        public async Task<QueryResultDto<JobOfferDto, JobOfferFilterDto> > GetAllOffers()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobOfferService.ListAllAsync();
            }
        }

        public async Task<IEnumerable<JobOfferDto>> GetAllOffersOfEmployer(int employerId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobOfferService.GetByEmployer(employerId);
            }
        }

        public async Task<IEnumerable<JobOfferDto>> GetOffersBySkill(int skillId)
        {
            using (UnitOfWorkProvider.Create())
            {
                SkillTagDto skill = await skillService.GetAsync(skillId, false);
                if (skill == null)
                {
                    return null;
                }

                return await jobOfferService.GetBySkills(skill);
            }
        }

        public async Task<IEnumerable<JobOfferDto>> GetOffersByName(string name)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobOfferService.GetByName(name);
            }
        }

        public async Task<IEnumerable<JobOfferDto>> GetOffersByFilter(JobOfferFilterDto filter)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await jobOfferService.GetFiltered(filter);
            }
        }

        public async Task<IList<JobOfferDto>> GetRecommendedOffersForUser(int userId, int numberOfResults)
        {
            using (UnitOfWorkProvider.Create())
            {
                var user = await userService.GetAsync(userId);
                var jobOffers = (await jobOfferService.ListAllAsync()).Items;

                // if user doesn't have skills return random job offers, otherwise return offers that fits user profile
                if (user.Skills.IsNullOrEmpty())
                {
                    return jobOfferRecommendationService.GetRandomOffers(jobOffers, numberOfResults);
                }
                return jobOfferRecommendationService.GetBestOffersForUser(user, jobOffers, numberOfResults);
            }
        }

        #endregion

        #region Management - employer only

        public async Task CreateJobOffer(JobOfferCreateDto jobOfferCreate)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                await jobOfferService.Create(jobOfferCreate);
                await unitOfWork.Commit();
            }
        }

        public async Task<bool> DeleteJobOffer(int entityId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await jobOfferService.GetAsync(entityId, false)) == null)
                {
                    return false;
                }
                jobOfferService.Delete(entityId);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> EditJobOffer(JobOfferDto jobOffer)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await jobOfferService.GetAsync(jobOffer.Id, false)) == null)
                {
                    return false;
                }
                await jobOfferService.Update(jobOffer);
                await uow.Commit();
                return true;
            }
        }

        #endregion
    }
}