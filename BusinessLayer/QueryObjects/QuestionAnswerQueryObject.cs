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
    public class QuestionAnswerQueryObject : QueryObjectBase<QuestionAnswerDto, QuestionAnswer, QuestionAnswerFilterDto, IQuery<QuestionAnswer>>
    {
        public QuestionAnswerQueryObject(IMapper mapper, IQuery<QuestionAnswer> query) : base(mapper, query)
        {
        }

        protected override IQuery<QuestionAnswer> ApplyWhereClause(IQuery<QuestionAnswer> query, QuestionAnswerFilterDto filter)
        {
            List<IPredicate> predicates = new List<IPredicate>();

            if (filter.ApplicationId != null)
                predicates.Add(new SimplePredicate(nameof(QuestionAnswer.ApplicationId), ValueComparingOperator.Equal, filter.ApplicationId));

            if (filter.QuestionId != null)
                predicates.Add(new SimplePredicate(nameof(QuestionAnswer.QuestionId), ValueComparingOperator.Equal, filter.QuestionId));

            return MergePredicates(predicates);
        }
    }
}

