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
    public class ApplicantQueryObject : QueryObjectBase<ApplicantDto, Applicant, ApplicantFilterDto, IQuery<Applicant>>
    {
        public ApplicantQueryObject(IMapper mapper, IQuery<Applicant> query) : base(mapper, query)
        {
        }

        protected override IQuery<Applicant> ApplyWhereClause(IQuery<Applicant> query, ApplicantFilterDto filter)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            if (!string.IsNullOrWhiteSpace(filter.FirstName))
            {
                predicates.Add(new SimplePredicate(nameof(Applicant.FirstName), ValueComparingOperator.Equal,
                    filter.FirstName));
            }
            if (!string.IsNullOrWhiteSpace(filter.LastName))
            {
                predicates.Add(
                    new SimplePredicate(nameof(User.LastName), ValueComparingOperator.Equal, filter.LastName));
            }
            if (!string.IsNullOrWhiteSpace(filter.MiddleName))
            {
                predicates.Add(new SimplePredicate(nameof(Applicant.MiddleName), ValueComparingOperator.Equal,
                    filter.MiddleName));
            }
            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                predicates.Add(new SimplePredicate(nameof(Applicant.Email), ValueComparingOperator.Equal, filter.Email));
            }
            if (!string.IsNullOrWhiteSpace(filter.PhoneNumber))
            {
                predicates.Add(new SimplePredicate(nameof(Applicant.PhoneNumber), ValueComparingOperator.Equal,
                    filter.PhoneNumber));
            }

            if (predicates.Count > 0)
            {
                return query.Where(new CompositePredicate(predicates));
            }

            return query;
        }
    }
}
