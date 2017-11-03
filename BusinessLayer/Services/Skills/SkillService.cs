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


namespace BusinessLayer.Services.Skills
{
    public class SkillService : CrudQueryServiceBase<SkillTag, SkillTagDto, SkillTagFilterDto>, ISkillService
    {
        public SkillService(IMapper mapper, IRepository<SkillTag> repository,
            QueryObjectBase<SkillTagDto, SkillTag, SkillTagFilterDto, IQuery<SkillTag>> quoryObject)
            : base(mapper, repository, quoryObject)
        {
        }

        protected override async Task<SkillTag> GetWithIncludesAsync(int entityId)
        {
            return await Repository.GetAsync(entityId, nameof(SkillTag.JobOffers), nameof(User.JobApplications));
        }

        public async Task<SkillTagDto> GetByName(string name)
        {
            var queryResult = await Query.ExecuteQuery(new SkillTagFilterDto {Name = name});
            return queryResult.Items.SingleOrDefault();
        }
    }
}