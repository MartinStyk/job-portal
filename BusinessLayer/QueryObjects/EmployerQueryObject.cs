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
    public class EmployerQueryObject : QueryObjectBase<EmployerDto, Employer, EmployerFilterDto, IQuery<Employer>>
    {
        public EmployerQueryObject(IMapper mapper, IQuery<Employer> query) : base(mapper, query)
        {
        }

        protected override IQuery<Employer> ApplyWhereClause(IQuery<Employer> query, EmployerFilterDto filter)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                predicates.Add(new SimplePredicate(nameof(Employer.Email), ValueComparingOperator.Equal,
                    filter.Email));
            }
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                predicates.Add(new SimplePredicate(nameof(Employer.Name), ValueComparingOperator.Equal,
                    filter.Name));
            }

            if (predicates.Count > 0)
            {
                return query.Where(new CompositePredicate(predicates));
            }

            return query;
        }
    }
}
