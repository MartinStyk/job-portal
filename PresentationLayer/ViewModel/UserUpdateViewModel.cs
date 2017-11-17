using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.DataTransferObjects;

namespace PresentationLayer.ViewModel
{
    public class UserUpdateViewModel
    {
        public IEnumerable<SelectListItem> AllSkills { get; set; }
        public UserDto UserDto { get; set; }
    }
}