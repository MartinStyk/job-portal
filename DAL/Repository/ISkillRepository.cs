
using DAL.Entities;
using Infrastructure.Repository;

namespace DAL.Repository
{
    public interface ISkillRepository : IRepository<SkillTag>
    {
        SkillTag GetByName(string name);
    }
}
