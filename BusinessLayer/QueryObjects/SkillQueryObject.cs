using AutoMapper;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.QueryObjects.Common;
using DAL.Entities;
using Infrastructure.Query;
using Infrastructure.Query.Predicates;
using Infrastructure.Query.Predicates.Operators;

namespace BusinessLayer.QueryObjects
{
    public class SkillQueryObject : QueryObjectBase<SkillTagDto, SkillTag, SkillTagFilterDto, IQuery<SkillTag>>
    {
        public SkillQueryObject(IMapper mapper, IQuery<SkillTag> query) : base(mapper, query)
        {
        }

        protected override IQuery<SkillTag> ApplyWhereClause(IQuery<SkillTag> query, SkillTagFilterDto filter)
        {
            if (string.IsNullOrWhiteSpace(filter.Name))
            {
                return query;
            }
            
            return query.Where(new SimplePredicate(nameof(SkillTag.Name), ValueComparingOperator.Equal, filter.Name));
        }
    }
}