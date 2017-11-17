using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades;

namespace PresentationLayer.Helpers
{
    public class SkillSelectListHelper
    {
        public SkillFacade SkillFacade { get; set; }

        public async Task<IList<SelectListItem>> Get(UserDto userDto = null)
        {
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            foreach (SkillTagDto skill in (await SkillFacade.GetAllSkillsAsync()).Items)
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = skill.Name,
                    Value = skill.Name,
                    Selected = userDto != null && userDto.Skills != null && userDto.Skills.Contains(skill.Name)
                };
                listSelectListItems.Add(selectList);
            }

            return listSelectListItems;
        }
    }
}