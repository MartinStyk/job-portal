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
        public async Task<ActionResult> Index()
        {
            var users = await UserFacade.GetAllUsersAsync();
            return View(users.Items);
        }

        // GET: User/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var user = await UserFacade.GetById(id);
            return View(user);
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
            return await Edit(-1, model);
        }

        // GET: User/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(int id)
        {
            var user = await UserFacade.GetById(id);

            var model = new UserUpdateViewModel
            {
                UserDto = user,
                AllSkills = await SkillSelectListHelper.Get(user)
            };

            return View(model);
        }

        // POST: User/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Edit(int id, UserUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await UserFacade.Update(model.UserDto);
                return RedirectToAction("Index");
            }

            model.AllSkills = await SkillSelectListHelper.Get(model.UserDto);
            return View(model);
        }

        // GET: User/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                await UserFacade.DeleteUser(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}