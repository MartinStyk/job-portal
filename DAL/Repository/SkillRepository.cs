﻿using System;
using DAL.Entities;
using Infrastructure.EntityFramework.Repository;
using Infrastructure.UnitOfWork;

namespace DAL.Repository
{
    public class SkillRepository : EntityFrameworkRepository<SkillTag, int>
    {
        public SkillRepository(IUnitOfWorkProvider provider) : base(provider)
        {
        }
    }
}