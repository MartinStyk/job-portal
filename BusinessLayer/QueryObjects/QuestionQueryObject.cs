using System;
using System.Collections.Generic;
using System.Linq;
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
    public class QuestionQueryObject : QueryObjectBase<QuestionDto, Question, QuestionFilterDto, IQuery<Question>>
    {
        public QuestionQueryObject(IMapper mapper, IQuery<Question> query) : base(mapper, query)
        {
        }

        protected override IQuery<Question> ApplyWhereClause(IQuery<Question> query, QuestionFilterDto filter)
        {
            if (filter.Keywords == null || !filter.Keywords.Any())
            {
                return query;
            }
            var predicates = new List<IPredicate>(filter.Keywords
                .Select(keyword => new SimplePredicate(
                    nameof(Question.Text),
                    ValueComparingOperator.StringContains,
                    keyword)));
            var predicate = new CompositePredicate(predicates, LogicalOperator.OR);
            return query.Where(predicate);
        }
    }
}