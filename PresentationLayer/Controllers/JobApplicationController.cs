using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Facades;

namespace PresentationLayer.Controllers
{
    public class JobApplicationController : Controller
    {
        public JobApplicationFacade JobApplicationFacade { get; set; }

        // GET: JobApplication
        public async Task<ActionResult> Index()
        {
            var applications = (await JobApplicationFacade.GetAllApplications()).Items;
            return View(applications);
        }

        // GET: JobApplication/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var application = await JobApplicationFacade.GetApplication(id);
            return View(application);
        }

        // GET: JobApplication/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobApplication/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: JobApplication/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: JobApplication/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: JobApplication/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: JobApplication/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
