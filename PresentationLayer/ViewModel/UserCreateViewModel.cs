using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.DataTransferObjects;

namespace PresentationLayer.ViewModel
{
    public class UserCreateViewModel
    {
        public IEnumerable<SelectListItem> AllSkills { get; set; }
        public UserCreateDto UserCreateDto { get; set; }
    }
}