using System;
using DAL.Entities;
using Infrastructure.EntityFramework.Repository;
using Infrastructure.UnitOfWork;

namespace DAL.Repository
{
    public class QuestionAnswerRepository : EntityFrameworkRepository<QuestionAnswer, int>
    {
        public QuestionAnswerRepository(IUnitOfWorkProvider provider) : base(provider)
        {
        }
    }
}