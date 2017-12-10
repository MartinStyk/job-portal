using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades;
using PresentationLayer.Helpers;
using PresentationLayer.ViewModel;

namespace PresentationLayer.Controllers
{
    public class UserController : Controller
    {
        public UserFacade UserFacade { get; set; }

        public SkillSelectListHelper SkillSelectListHelper { get; set; }


        // GET: User
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> Index()
        {
            var users = await UserFacade.GetAllUsersAsync();
            return View(users.Items);
        }

        // GET: User/EditCurrentUser
        [Authorize(Roles = "User")]
        public async Task<ActionResult> EditCurrentUser()
        {
            var user = await UserFacade.GetUserByEmail(User.Identity.Name);

            var model = new UserUpdateViewModel
            {
                UserDto = user,
                AllSkills = await SkillSelectListHelper.Get(user)
            };

            return View("Edit", model);
        }

        // POST: User/EditCurrentUser
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<ActionResult> EditCurrentUser(UserUpdateViewModel model)
        {
            var currentUser = await UserFacade.GetUserByEmail(User.Identity.Name);


            if (ModelState.IsValid)
            {
                await UserFacade.Update(model.UserDto, currentUser);
                return RedirectToAction("Index", "Home");
            }

            model.AllSkills = await SkillSelectListHelper.Get(model.UserDto);
            return View("Edit", model);
        }
    }
}