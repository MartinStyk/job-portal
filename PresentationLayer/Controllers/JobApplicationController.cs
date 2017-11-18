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
    public class JobApplicationController : Controller
    {
        public JobApplicationFacade JobApplicationFacade { get; set; }
        public JobOfferFacade JobOfferFacade { get; set; }

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
        public async Task<ActionResult> Create(int jobOfferId)
        {
            var offer = await JobOfferFacade.GetOffer(jobOfferId);

            var model = new JobApplicationCreateDto
            {
                // TODO current user (if logged) 
                // Applicant = 
                JobOffer = offer,
                JobOfferId = offer.Id
            };

            return View(model);
        }

        // POST: JobApplication/Create
        [HttpPost]
        public async Task<ActionResult> Create(JobApplicationCreateDto application)
        {
            if (ModelState.IsValid)
            {
                await JobApplicationFacade.CreateApplication(application);
                return RedirectToAction("Index");
            }

            var offer = await JobOfferFacade.GetOffer(application.JobOfferId);
            application.JobOffer = offer;
            application.JobOfferId = offer.Id;

            return View(application);
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
        public async Task<ActionResult> Delete(int id)
        {
            var application = await JobApplicationFacade.GetApplication(id);
            return View(application);
        }

        // POST: JobApplication/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                await JobApplicationFacade.DeleteApplication(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}