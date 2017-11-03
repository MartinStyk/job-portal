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


namespace BusinessLayer.Services.Employers
{
    public class EmployerService : CrudQueryServiceBase<Employer, EmployerDto, EmployerFilterDto>, IEmployerService
    {
        public EmployerService(IMapper mapper, IRepository<Employer> repository,
            QueryObjectBase<EmployerDto, Employer, EmployerFilterDto, IQuery<Employer>> quoryObject)
            : base(mapper, repository, quoryObject)
        {
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
            var queryResult = await Query.ExecuteQuery(new EmployerFilterDto { Email = mail});
            return queryResult.Items.FirstOrDefault();
        }
    }
}