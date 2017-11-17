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
    public class EmployerSelectListHelper
    {
        public EmployerFacade EmployerFacade { get; set; }

        public async Task<IList<SelectListItem>> Get()
        {
            List<SelectListItem> listSelectListItems = new List<SelectListItem>();

            foreach (EmployerDto employer in (await EmployerFacade.GetAllEmployersAsync()).Items)
            {
                SelectListItem selectList = new SelectListItem()
                {
                    Text = employer.Name,
                    Value = employer.Id.ToString()
                };
                listSelectListItems.Add(selectList);
            }

            return listSelectListItems;
        }
    }
}