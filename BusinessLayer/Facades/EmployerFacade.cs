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
using BusinessLayer.Services.JobApplications;
using BusinessLayer.Services.JobOffers;
using BusinessLayer.Services.Questions;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades
{
    public class EmployerFacade : FacadeBase
    {
        private readonly IEmployerService employerService;


        public EmployerFacade(IUnitOfWorkProvider unitOfWorkProvider,
            IEmployerService employerService)
            : base(unitOfWorkProvider)
        {
            this.employerService = employerService;
        }

        // TODO registration
        public async Task Register(EmployerDto employer)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                employerService.Create(employer);
                await unitOfWork.Commit();
            }
        }

        public async Task Update(EmployerDto employer)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                await employerService.Update(employer);
                await unitOfWork.Commit();
            }
        }

        public async Task<QueryResultDto<EmployerDto, EmployerFilterDto>> GetAllEmployersAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await employerService.ListAllAsync();
            }
        }

        public async Task<EmployerDto> GetEmployerByEmail(String email)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await employerService.GetByEmail(email);
            }
        }

        public async Task<EmployerDto> GetEmployerByName(String name)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await employerService.GetByName(name);
            }
        }


        public async Task<QueryResultDto<EmployerDto, EmployerFilterDto>> GetEmployerForFilter(EmployerFilterDto employerFilterDto)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await employerService.GetFiltered(employerFilterDto);
            }
        }
    }
}