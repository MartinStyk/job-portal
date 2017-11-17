using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades;

namespace PresentationLayer.Controllers
{
    public class EmployerController : Controller
    {
        public EmployerFacade EmployerFacade { get; set; }

        // GET: Employer
        public async Task<ActionResult> Index()
        {
            var employers = (await EmployerFacade.GetAllEmployersAsync()).Items;
            return View(employers);
        }

        // GET: Employer/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var employers = await EmployerFacade.GetEmployerById(id);
            return View(employers);
        }

        // GET: Employer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employer/Create
        [HttpPost]
        public async Task<ActionResult> Create(EmployerCreateDto employerCreateDto)
        {
            if (ModelState.IsValid)
            {
                await EmployerFacade.Register(employerCreateDto);
                return RedirectToAction("Index");
            }
            return View(employerCreateDto);
        }

        // GET: Employer/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var user = await EmployerFacade.GetEmployerById(id);
            return View(user);
        }

        // POST: Employer/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, EmployerDto employerDto)
        {
            if (ModelState.IsValid)
            {
                await EmployerFacade.Update(employerDto);
                return RedirectToAction("Index");
            }

            return View(employerDto);
        }

        // GET: Employer/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var employer = await EmployerFacade.GetEmployerById(id);
            return View(employer);
        }

        // POST: Employer/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                await EmployerFacade.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}