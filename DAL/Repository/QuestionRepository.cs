﻿using System;
using DAL.Entities;
using Infrastructure.EntityFramework.Repository;
using Infrastructure.UnitOfWork;

namespace DAL.Repository
{
    public class QuestionRepository : EntityFrameworkRepository<Question, int>
    {
        public QuestionRepository(IUnitOfWorkProvider provider) : base(provider)
        {
        }
    }
}