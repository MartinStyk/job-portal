using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.Employers;
using BusinessLayer.Services.JobOffers;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades
{
    public class EmployerJobManagementFacade : FacadeBase
    {
        private readonly IEmployerService employerService;
        private readonly IJobOfferService jobOfferService;



        public EmployerJobManagementFacade(IUnitOfWorkProvider unitOfWorkProvider,
            IEmployerService employerService, IJobOfferService jobOfferService) : base(unitOfWorkProvider)
        {
            this.employerService = employerService;
            this.jobOfferService = jobOfferService;

        }

        public async Task CreateJobOffer(JobOfferDto jobOffer)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                jobOfferService.Create(jobOffer);
                await unitOfWork.Commit();
            }
        }

    }
}