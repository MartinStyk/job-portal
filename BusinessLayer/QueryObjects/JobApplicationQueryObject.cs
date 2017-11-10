using System;
using System.Collections.Generic;
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
    public class JobApplicationQueryObject : QueryObjectBase<JobApplicationDto, JobApplication, JobApplicationFilterDto, IQuery<JobApplication>>
    {
        public JobApplicationQueryObject(IMapper mapper, IQuery<JobApplication> query) : base(mapper, query)
        {
        }

        protected override IQuery<JobApplication> ApplyWhereClause(IQuery<JobApplication> query, JobApplicationFilterDto filter)
        {
            var definedPredicates = new List<IPredicate>();

            if (filter.JobOfferId != null)
                definedPredicates.Add(new SimplePredicate(nameof(JobApplication.JobOfferId), ValueComparingOperator.Equal, filter.JobOfferId));

            if (filter.ApplicantId != null)
                definedPredicates.Add(new SimplePredicate(nameof(JobApplication.ApplicantId), ValueComparingOperator.Equal, filter.ApplicantId));

            if (filter.JobApplicationStatus != null)
                definedPredicates.Add(new SimplePredicate(nameof(JobApplication.JobApplicationStatus), ValueComparingOperator.Equal, filter.JobApplicationStatus));


            return MergePredicates(definedPredicates);
        }
    }
}