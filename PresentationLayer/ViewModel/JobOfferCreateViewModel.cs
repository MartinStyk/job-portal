using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.DataTransferObjects;

namespace PresentationLayer.ViewModel
{
    public class JobOfferCreateViewModel
    {
        [Range(0,10)]
        public int NumberOfQuestions { get; set; }
        public IEnumerable<SelectListItem> AllSkills { get; set; }
        public IEnumerable<SelectListItem> AllEmployers { get; set; }

        public JobOfferCreateDto JobOfferCreateDto { get; set; }
    }
}