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
        public SkillFacade SkillFacade { get; set; }

        public SkillSelectListHelper SkillSelectListHelper { get; set; }


        // GET: User
        public async Task<ActionResult> Index()
        {
            var users = await UserFacade.GetAllUsersAsync();
            return View(users.Items);
        }

        // GET: User/Details/5
        [Route("Details")]
        public async Task<ActionResult> Details(int id)
        {
            var user = await UserFacade.GetById(id);
            return View(user);
        }

        // GET: User/Create
        public async Task<ActionResult> Create()
        {
            var model = new UserCreateViewModel
            {
                AllSkills = await SkillSelectListHelper.Get()
            };

            return View(model);
        }

        // POST: User/Create
        [HttpPost]
        public async Task<ActionResult> Create(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await UserFacade.Register(model.UserCreateDto);
                return RedirectToAction("Index");
            }
            model.AllSkills = await SkillSelectListHelper.Get();
            return View(model);
        }

        // GET: User/Edit/5
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        [HttpPost]
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