using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class JobOfferQueryObject : QueryObjectBase<JobOfferDto, JobOffer, JobOfferFilterDto, IQuery<JobOffer>>
    {
        public JobOfferQueryObject(IMapper mapper, IQuery<JobOffer> query) : base(mapper, query)
        {
        }

        protected override IQuery<JobOffer> ApplyWhereClause(IQuery<JobOffer> query, JobOfferFilterDto filter)
        {
            var definedPredicates = new List<IPredicate>();

            if (!string.IsNullOrWhiteSpace(filter.Name))
                definedPredicates.Add(new SimplePredicate(nameof(JobOffer.Name), ValueComparingOperator.StringContains, filter.Name));

            if (filter.EmployerId != null) 
                definedPredicates.Add(new SimplePredicate(nameof(JobOffer.EmployerId), ValueComparingOperator.Equal, filter.EmployerId));

            return MergePredicates(definedPredicates);
        }
    }
}