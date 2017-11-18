using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades;
using PresentationLayer.Helpers;
using PresentationLayer.ViewModel;

namespace PresentationLayer.Controllers
{
    public class JobOfferController : Controller
    {
        public JobOfferFacade JobOfferFacade { get; set; }
        public SkillSelectListHelper SkillSelectListHelper { get; set; }
        public EmployerSelectListHelper EmployerSelectListHelper { get; set; }


        // GET: JobOffer
        public async Task<ActionResult> Index()
        {
            var offers = (await JobOfferFacade.GetAllOffers()).Items;
            return View(offers);
        }

        // GET: JobOffer/OffersOfEmployer/2
        public async Task<ActionResult> OffersOfEmployer(int id)
        {
            var offers = await JobOfferFacade.GetAllOffersOfEmployer(id);
            return View("Index", offers);
        }

        // GET: JobOffer/OffersBySkill/2
        public async Task<ActionResult> OffersBySkill(int id)
        {
            var offers = await JobOfferFacade.GetOffersBySkill(id);
            return View("Index", offers);
        }

        // GET: JobOffer/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var offer = await JobOfferFacade.GetOffer(id);
            return View(offer);
        }

        // GET: JobOffer/Create
        public async Task<ActionResult> Create()
        {
            var model = new JobOfferCreateViewModel
            {
                AllSkills = await SkillSelectListHelper.Get(),
                AllEmployers = await EmployerSelectListHelper.Get()
            };

            return View(model);
        }

        // POST: JobOffer/Create
        [HttpPost]
        public async Task<ActionResult> Create(JobOfferCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await JobOfferFacade.CreateJobOffer(model.JobOfferCreateDto);
                return RedirectToAction("Index");
            }

            model.AllSkills = await SkillSelectListHelper.Get();
            model.AllEmployers = await EmployerSelectListHelper.Get();

            return View(model);
        }

        // GET: JobOffer/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var model = new JobOfferCreateViewModel
            {
                AllSkills = await SkillSelectListHelper.Get(),
                AllEmployers = await EmployerSelectListHelper.Get(),
                JobOfferCreateDto = new JobOfferCreateDto(await JobOfferFacade.GetOffer(id))
            };

            return View(model);
        }

        // POST: JobOffer/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, JobOfferCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await JobOfferFacade.EditJobOffer(model.JobOfferCreateDto);
                return RedirectToAction("Index");
            }

            model.AllSkills = await SkillSelectListHelper.Get();
            model.AllEmployers = await EmployerSelectListHelper.Get();

            return View(model);
        }

        // GET: JobOffer/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var offer = await JobOfferFacade.GetOffer(id);
            return View(offer);
        }

        // POST: JobOffer/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                await JobOfferFacade.DeleteJobOffer(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}