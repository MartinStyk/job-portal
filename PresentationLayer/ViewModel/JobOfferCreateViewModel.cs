using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.DataTransferObjects;

namespace PresentationLayer.ViewModel
{
    public class JobOfferCreateViewModel
    {
        public IEnumerable<SelectListItem> AllSkills { get; set; }
        public IEnumerable<SelectListItem> AllEmployers { get; set; }

        public JobOfferCreateDto JobOfferCreateDto { get; set; }
    }
}