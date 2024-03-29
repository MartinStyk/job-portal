﻿using System;
using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.Employers;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades
{
    public class EmployerFacade : FacadeBase
    {
        private readonly IEmployerService employerService;

        public EmployerFacade(IUnitOfWorkProvider unitOfWorkProvider, IEmployerService employerService)
            : base(unitOfWorkProvider)
        {
            this.employerService = employerService;
        }

        // TODO registration
        public async Task Register(EmployerCreateDto employer)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                await employerService.Create(employer);
                await unitOfWork.Commit();
            }
        }

        public (bool success, string roles) Login(string mail, string password)
        {
            using (UnitOfWorkProvider.Create())
            {
                return employerService.AuthorizeEmployerAsync(mail, password);
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

        public async Task Delete(int id)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                employerService.Delete(id);
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

        public async Task<EmployerDto> GetEmployerById(int id)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await employerService.GetAsync(id);
            }
        }


        public async Task<QueryResultDto<EmployerDto, EmployerFilterDto>> GetEmployerForFilter(
            EmployerFilterDto employerFilterDto)
        {
            using (UnitOfWorkProvider.Create())
            {
                return await employerService.GetFiltered(employerFilterDto);
            }
        }
    }
}