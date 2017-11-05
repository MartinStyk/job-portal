using System.Threading.Tasks;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Common;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Facades.Common;
using BusinessLayer.Services.Skills;
using Infrastructure.UnitOfWork;

namespace BusinessLayer.Facades
{
    public class SkillFacade : FacadeBase
    {
        private readonly ISkillService skillService;

        public SkillFacade(IUnitOfWorkProvider unitOfWorkProvider, ISkillService skillService) : base(unitOfWorkProvider)
        {
            this.skillService = skillService;
        }

        public async Task CreateSkill(SkillTagDto skill)
        {
            using (var unitOfWork = UnitOfWorkProvider.Create())
            {
                skillService.Create(skill);
                await unitOfWork.Commit();
            }
        }

        public async Task<bool> EditSkill(SkillTagDto skill)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await skillService.GetAsync(skill.Id, false)) == null)
                {
                    return false;
                }
                await skillService.Update(skill);
                await uow.Commit();
                return true;
            }
        }

        public async Task<bool> DeleteSkill(int skillId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                if ((await skillService.GetAsync(skillId, false)) == null)
                {
                    return false;
                }
                skillService.Delete(skillId);
                await uow.Commit();
                return true;
            }
        }

        public async Task<QueryResultDto<SkillTagDto, SkillTagFilterDto>> GetAllSkillsAsync()
        {
            using (UnitOfWorkProvider.Create())
            {
                return await skillService.ListAllAsync();
            }
        }
    }
}
