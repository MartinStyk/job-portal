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
    public class UserQueryObject : QueryObjectBase<UserDto, User, UserFilterDto, IQuery<User>>
    {
        public UserQueryObject(IMapper mapper, IQuery<User> query) : base(mapper, query)
        {
        }

        protected override IQuery<User> ApplyWhereClause(IQuery<User> query, UserFilterDto filter)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            if (!string.IsNullOrWhiteSpace(filter.FirstName))
            {
                predicates.Add(new SimplePredicate(nameof(User.FirstName), ValueComparingOperator.Equal,
                    filter.FirstName));
            }
            if (!string.IsNullOrWhiteSpace(filter.LastName))
            {
                predicates.Add(
                    new SimplePredicate(nameof(User.LastName), ValueComparingOperator.Equal, filter.LastName));
            }
            if (!string.IsNullOrWhiteSpace(filter.MiddleName))
            {
                predicates.Add(new SimplePredicate(nameof(User.MiddleName), ValueComparingOperator.Equal,
                    filter.MiddleName));
            }
            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                predicates.Add(new SimplePredicate(nameof(User.Email), ValueComparingOperator.Equal, filter.Email));
            }
            if (!string.IsNullOrWhiteSpace(filter.PhoneNumber))
            {
                predicates.Add(new SimplePredicate(nameof(User.PhoneNumber), ValueComparingOperator.Equal,
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