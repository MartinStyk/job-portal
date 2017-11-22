using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Entities;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects.Common;
using BusinessLayer.Services.Common;
using BusinessLayer.Services.Skills;
using DAL.Repository;
using Infrastructure.Query;
using Infrastructure.Repository;


namespace BusinessLayer.Services.Users
{
    public class UserService : CrudQueryServiceBase<User, UserDto, UserFilterDto>, IUserService
    {
        private readonly ISkillRepository skillRepository;

        public UserService(IMapper mapper, IRepository<User> repository,
            QueryObjectBase<UserDto, User, UserFilterDto, IQuery<User>> quoreObject, ISkillRepository skillRepository)
            : base(mapper, repository, quoreObject)
        {
            this.skillRepository = skillRepository;
        }

        public override int Create(UserDto entityDto)
        {
            User user = Mapper.Map<User>(entityDto);
            user.Skills = new List<SkillTag>();

            if (entityDto.Skills != null)
            {
                foreach (var skillName in entityDto.Skills)
                {
                    var skill = skillRepository.GetByName(skillName);
                    user.Skills.Add(skill);
                }
            }

            Repository.Create(user);
            return user.Id;
        }

        public override async Task Update(UserDto entityDto)
        {
            var entity = await GetWithIncludesAsync(entityDto.Id);
            Mapper.Map(entityDto, entity);

            entity.Skills = new List<SkillTag>();

            if (entityDto.Skills != null)
            {
                foreach (var skillName in entityDto.Skills)
                {
                    var skill = skillRepository.GetByName(skillName);
                    entity.Skills.Add(skill);
                }
            }

            Repository.Update(entity);
        }

        protected override async Task<User> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId, nameof(User.JobApplications), nameof(User.Skills));
        }

        public async Task<UserDto> GetByEmailAsync(string email)
        {
            var queryResult = await Query.ExecuteQuery(new UserFilterDto {Email = email});
            return queryResult.Items.SingleOrDefault();
        }

        public async Task<QueryResultDto<UserDto, UserFilterDto>> GetFiltered(UserFilterDto filter)
        {
            return await Query.ExecuteQuery(filter);
        }
    }
}