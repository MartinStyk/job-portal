using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security;
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
        public EmployerFacade EmployerFacade { get; set; }
        public SkillSelectListHelper SkillSelectListHelper { get; set; }


        // GET: JobOffer
        public async Task<ActionResult> Index()
        {
            var offers = (await JobOfferFacade.GetAllOffers()).Items;
            return View(offers);
        }

        // GET: JobOffer/OffersOfEmployer/2
        [AllowAnonymous]
        public async Task<ActionResult> OffersOfEmployer(int id)
        {
            var offers = await JobOfferFacade.GetAllOffersOfEmployer(id);
            return View("Index", offers);
        }

        // GET: JobOffer/OffersOfCurrentEmployer
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> OffersOfCurrentEmployer()
        {
            var employer = await EmployerFacade.GetEmployerByEmail(User.Identity.Name);
            var offers = await JobOfferFacade.GetAllOffersOfEmployer(employer.Id);
            return View("Index", offers);
        }

        // GET: JobOffer/OffersBySkill/2
        [AllowAnonymous]
        public async Task<ActionResult> OffersBySkill(int id)
        {
            var offers = await JobOfferFacade.GetOffersBySkill(id);
            return View("Index", offers);
        }

        // GET: JobOffer/Details/5
        [AllowAnonymous]
        public async Task<ActionResult> Details(int id)
        {
            var offer = await JobOfferFacade.GetOffer(id);
            return View(offer);
        }

        // GET: JobOffer/Create
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> Create()
        {
            var model = new JobOfferCreateViewModel
            {
                AllSkills = await SkillSelectListHelper.Get(),
                Employer = await EmployerFacade.GetEmployerByEmail(User.Identity.Name),
                NumberOfQuestions = 1
            };

            return View(model);
        }

        // POST: JobOffer/Create
        [HttpPost]
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> Create(JobOfferCreateViewModel model)
        {
            var currentEmployer = await EmployerFacade.GetEmployerByEmail(User.Identity.Name);

            if (!CheckQuestionChange(model) && ModelState.IsValid)
            {
                model.JobOfferCreateDto.EmployerId = currentEmployer.Id;
                await JobOfferFacade.CreateJobOffer(model.JobOfferCreateDto);
                return RedirectToAction("OffersOfCurrentEmployer");
            }

            model.AllSkills = await SkillSelectListHelper.Get();
            model.Employer = currentEmployer;


            return View(model);
        }

        // GET: JobOffer/Edit/5
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> Edit(int id)
        {
            var jobOfferCreateDto = new JobOfferCreateDto(await JobOfferFacade.GetOffer(id));
            var currentEmployer = await EmployerFacade.GetEmployerByEmail(User.Identity.Name);

            if (currentEmployer.Id != jobOfferCreateDto.EmployerId)
                throw new ArgumentException();

            var model = new JobOfferCreateViewModel
            {
                AllSkills = await SkillSelectListHelper.Get(),
                Employer = currentEmployer,
                JobOfferCreateDto = jobOfferCreateDto
            };

            return View(model);
        }

        // POST: JobOffer/Edit/5
        [HttpPost]
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> Edit(int id, JobOfferCreateViewModel model)
        {
            var currentEmployer = await EmployerFacade.GetEmployerByEmail(User.Identity.Name);

            if (ModelState.IsValid)
            {
                model.JobOfferCreateDto.EmployerId = currentEmployer.Id;
                await JobOfferFacade.EditJobOffer(model.JobOfferCreateDto, currentEmployer);
                return RedirectToAction("OffersOfCurrentEmployer");
            }

            model.AllSkills = await SkillSelectListHelper.Get();
            model.Employer = currentEmployer;
            return View(model);
        }

        // GET: JobOffer/Delete/5
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> Delete(int id)
        {
            var offer = await JobOfferFacade.GetOffer(id);
            var currentEmployer = await EmployerFacade.GetEmployerByEmail(User.Identity.Name);

            if (currentEmployer.Id != offer.EmployerId)
                throw new ArgumentException();

            return View(offer);
        }

        // POST: JobOffer/Delete/5
        [HttpPost]
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            var currentEmployer = await EmployerFacade.GetEmployerByEmail(User.Identity.Name);

            try
            {
                await JobOfferFacade.DeleteJobOffer(id, currentEmployer);
                return RedirectToAction("OffersOfCurrentEmployer");
            }
            catch
            {
                return View();
            }
        }

        #region Private

        private bool CheckQuestionChange(JobOfferCreateViewModel model)
        {
            if (Request.Form["ChangeQuestions"] != null)
            {
                // handle adding questions
                if (model.JobOfferCreateDto.Questions == null)
                    model.JobOfferCreateDto.Questions = new List<QuestionDto>();
                while (model.JobOfferCreateDto.Questions.Count < model.NumberOfQuestions)
                {
                    model.JobOfferCreateDto.Questions.Add(new QuestionDto());
                }
                return true;
            }
            return false;
        }

        #endregion
    }
}