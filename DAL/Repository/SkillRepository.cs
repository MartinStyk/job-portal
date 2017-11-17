using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Entities;
using Infrastructure.EntityFramework.Repository;
using Infrastructure.UnitOfWork;

namespace DAL.Repository
{
    public class SkillRepository : EntityFrameworkRepository<SkillTag>
    {
        public SkillRepository(IUnitOfWorkProvider provider) : base(provider)
        {
        }

        public SkillTag GetByName(string name)
        {
            return Context.Set<SkillTag>().SingleOrDefault(tag => tag.Name.Equals(name));
        }
    }
}