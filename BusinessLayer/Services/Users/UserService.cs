using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Entities;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using Infrastructure.Query;
using Infrastructure.Repository;


namespace BusinessLayer.Services.Users
{
    public class UserService : CrudQueryServiceBase<User, UserDto, UserFilterDto>, IUserService
    {
        public UserService(IMapper mapper, IRepository<User> repository,
            QueryObjectBase<UserDto, User, UserFilterDto, IQuery<User>> quoreObject)
            : base(mapper, repository, quoreObject)
        {
        }

        protected override async Task<User> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId, nameof(User.JobApplications), nameof(User.Skills));
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var queryResult = await Query.ExecuteQuery(new UserFilterDto { Email = email});
            return queryResult.Items.SingleOrDefault();
        }

        public async Task<QueryResultDto<UserDto, UserFilterDto>> GetFiltered(UserFilterDto filter)
        {
            return await Query.ExecuteQuery(filter);
        }
    }
}
