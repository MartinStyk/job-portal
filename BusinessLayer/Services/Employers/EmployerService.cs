using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Entities;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Auth;
using BusinessLayer.Services.Common;
using DAL.Repository;
using Infrastructure.Query;
using Infrastructure.Repository;

namespace BusinessLayer.Services.Employers
{
    public class EmployerService : CrudQueryServiceBase<Employer, EmployerDto, EmployerFilterDto>, IEmployerService
    {
        private IEmployerRepository employerRepository;
        private IAuthenticationService authenticationService;

        public EmployerService(IMapper mapper, IEmployerRepository repository,
            QueryObjectBase<EmployerDto, Employer, EmployerFilterDto, IQuery<Employer>> quoryObject, IAuthenticationService authenticationService)
            : base(mapper, repository, quoryObject)
        {
            this.employerRepository = repository;
            this.authenticationService = authenticationService;
        }

        public override int Create(EmployerDto entityDto)
        {
            throw new NotImplementedException("Use EmployerCreateDto");
        }

        public async Task<int> Create(EmployerCreateDto entityDto)
        {
            Employer employer = Mapper.Map<Employer>(entityDto);
           
            if ((await GetByEmail(entityDto.Email)) != null)
            {
                throw new ArgumentException();
            }

            var password = authenticationService.CreateHash(entityDto.Password);
            employer.PasswordHash = password.Item1;
            employer.PasswordSalt = password.Item2;
            employer.Roles = "Employer";

            Repository.Create(employer);
            return employer.Id;
        }


        protected override async Task<Employer> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId, nameof(Employer.JobOffers));
        }

        public async Task<EmployerDto> GetByName(string name)
        {
            var queryResult = await Query.ExecuteQuery(new EmployerFilterDto {Name = name});
            return queryResult.Items.FirstOrDefault();
        }

        public async Task<EmployerDto> GetByEmail(string mail)
        {
            var queryResult = await Query.ExecuteQuery(new EmployerFilterDto { Email = mail });
            return queryResult.Items.FirstOrDefault();
        }

        public async Task<QueryResultDto<EmployerDto, EmployerFilterDto>> GetFiltered(EmployerFilterDto filter)
        {
            return await Query.ExecuteQuery(filter);
        }

        public (bool success, string roles) AuthorizeEmployerAsync(string email, string password)
        {
            var employer = employerRepository.GetByEmail(email);

            var succ = employer != null && authenticationService.VerifyHashedPassword(employer.PasswordHash, employer.PasswordSalt, password);
            var roles = employer?.Roles ?? "";
            return (succ, roles);
        }
    }
}