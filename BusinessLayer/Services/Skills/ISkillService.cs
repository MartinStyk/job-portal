using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Services.Common;

namespace BusinessLayer.Services.Skills
{
    public interface ISkillService : ICrudService<SkillTagDto, SkillTagFilterDto>
    {
        /// <summary>
        /// Get Skill by name 
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>Skill with given name</returns>
        Task<SkillTagDto> GetByName(string name);

    }
}