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


namespace BusinessLayer.Services.Users
{
    public class UserService : CrudQueryServiceBase<User, UserDto, UserFilterDto>, IUserService
    {
        private readonly ISkillRepository skillRepository;
        private readonly IUserRepository userRepository;
        private readonly IAuthenticationService authenticationService;

        public UserService(IMapper mapper, IUserRepository repository,
            QueryObjectBase<UserDto, User, UserFilterDto, IQuery<User>> quoreObject, ISkillRepository skillRepository, IAuthenticationService authenticationService)
            : base(mapper, repository, quoreObject)
        {
            this.skillRepository = skillRepository;
            this.userRepository = repository;
            this.authenticationService = authenticationService;
        }

        public override int Create(UserDto entityDto)
        {
            throw new NotImplementedException("Use UserCreateDto");
        }
        
        public async Task<int> Create(UserCreateDto entityDto)
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

            if ((await GetByEmailAsync(entityDto.Email)) != null)
            {
                throw new ArgumentException();
            }

            var password = authenticationService.CreateHash(entityDto.Password);
            user.PasswordHash = password.Item1;
            user.PasswordSalt = password.Item2;
            user.Roles = "User";


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

        public (bool success, string roles) AuthorizeUserAsync(string email, string password)
        {
            var user = userRepository.GetByEmail(email);

            var succ = user != null && authenticationService.VerifyHashedPassword(user.PasswordHash, user.PasswordSalt, password);
            var roles = user?.Roles ?? "";
            return (succ, roles);
        }

    }
}