using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.DataTransferObjects.Filters;
using BusinessLayer.Facades;
using Castle.Core.Internal;

namespace PresentationLayer.Controllers
{
    public class JobApplicationController : Controller
    {
        public JobApplicationFacade JobApplicationFacade { get; set; }
        public JobOfferFacade JobOfferFacade { get; set; }
        public UserFacade UserFacade { get; set; }
        public EmployerFacade EmployerFacade { get; set; }


       // GET: JobApplication/Details/5
       [Authorize(Roles = "User, Employer")]
        public async Task<ActionResult> Details(int id)
        {
            var application = await JobApplicationFacade.GetApplication(id);
            return View(application);
        }

        // GET: JobApplication/Create
        [AllowAnonymous]
        public async Task<ActionResult> Create(int jobOfferId)
        {
            var offer = await JobOfferFacade.GetOffer(jobOfferId);
            var user = User.Identity.Name.IsNullOrEmpty()
                ? new ApplicantDto()
                : await UserFacade.GetUserByEmail(User.Identity.Name);

            var model = new JobApplicationCreateDto
            {
                Applicant = user,
                JobOffer = offer,
                JobOfferId = offer.Id
            };

            return View(model);
        }

        // POST: JobApplication/Create
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Create(JobApplicationCreateDto application)
        {
            if (ModelState.IsValid)
            {
                await JobApplicationFacade.CreateApplication(application);
                return RedirectToAction("Details", "JobOffer", new { id = application.JobOfferId });
            }

            var offer = await JobOfferFacade.GetOffer(application.JobOfferId);
            application.JobOffer = offer;
            application.JobOfferId = offer.Id;
            application.Applicant = User.Identity.Name.IsNullOrEmpty()
                ? new ApplicantDto()
                : await UserFacade.GetUserByEmail(User.Identity.Name);


            return View(application);
        }

        // GET: JobApplication/ApplicationsOfCurrentUser
        [Authorize(Roles = "User")]
        public async Task<ActionResult> ApplicationsOfCurrentUser()
        {
            var user = await UserFacade.GetUserByEmail(User.Identity.Name);
            var aplications = await JobApplicationFacade.GetByApplicant(user.Id);
            return View("Index", aplications.Items);
        }

        // GET: JobApplication/ApplicationsByJobOffer/2
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> ApplicationsByJobOffer(int id)
        {
            var emoloyer = await EmployerFacade.GetEmployerByEmail(User.Identity.Name);
            var job = await JobOfferFacade.GetOffer(id);

            if(job.Employer.Id != emoloyer.Id)
                throw new ArgumentException();

            var aplications = await JobApplicationFacade.GetByJobOffer(id);
            return View("Index", aplications.Items);
        }

        // GET: JobApplication/Delete/5
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> Delete(int id)
        {
            var emoloyer = await EmployerFacade.GetEmployerByEmail(User.Identity.Name);
            var application = await JobApplicationFacade.GetApplication(id);

            if(application.JobOffer.Employer.Id != emoloyer.Id)
                throw new ArgumentException();

            return View(application);
        }

        // POST: JobApplication/Delete/5
        [HttpPost]
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                await JobApplicationFacade.DeleteApplication(id, await EmployerFacade.GetEmployerByEmail(User.Identity.Name));
                return RedirectToAction("ApplicationsByJobOffer", new { id = (await JobApplicationFacade.GetApplication(id)).JobOffer.Id });
            }
            catch
            {
                return View();
            }
        }

        // GET: JobApplication/AcceptApplication/5
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> AcceptApplication(int id)
        {
            ViewBag.Action = "Accept application";
            var emoloyer = await EmployerFacade.GetEmployerByEmail(User.Identity.Name);
            var application = await JobApplicationFacade.GetApplication(id);

            if (application.JobOffer.Employer.Id != emoloyer.Id)
                throw new ArgumentException();

            return View("ChangeApplicationStatus", application);
        }

        // POST: JobApplication/AcceptApplication/5
        [HttpPost]
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> AcceptApplication(int id, FormCollection collection)
        {
            await JobApplicationFacade.AcceptApplication(id, await EmployerFacade.GetEmployerByEmail(User.Identity.Name));

            return RedirectToAction("ApplicationsByJobOffer", new {id = (await JobApplicationFacade.GetApplication(id)).JobOffer.Id});
        }

        // GET: JobApplication/AcceptApplicationAndCloseOther/5
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> AcceptApplicationAndCloseOther(int id)
        {
            ViewBag.Action = "Accept application and close all other";
            var emoloyer = await EmployerFacade.GetEmployerByEmail(User.Identity.Name);
            var application = await JobApplicationFacade.GetApplication(id);

            if (application.JobOffer.Employer.Id != emoloyer.Id)
                throw new ArgumentException();

            return View("ChangeApplicationStatus", application);
        }

        // POST: JobApplication/AcceptApplicationAndCloseOther/5
        [HttpPost]
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> AcceptApplicationAndCloseOther(int id, FormCollection collection)
        {
            await JobApplicationFacade.AcceptOnlyThisApplication(id, await EmployerFacade.GetEmployerByEmail(User.Identity.Name));

            return RedirectToAction("ApplicationsByJobOffer", new { id = (await JobApplicationFacade.GetApplication(id)).JobOffer.Id });
        }


        // GET: JobApplication/RejectApplication/5
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> RejectApplication(int id)
        {
            ViewBag.Action = "Reject application";

            var emoloyer = await EmployerFacade.GetEmployerByEmail(User.Identity.Name);
            var application = await JobApplicationFacade.GetApplication(id);

            if (application.JobOffer.Employer.Id != emoloyer.Id)
                throw new ArgumentException();

            return View("ChangeApplicationStatus", application);
        }

        // POST: JobApplication/RejectApplication/5
        [HttpPost]
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> RejectApplication(int id, FormCollection collection)
        {
            await JobApplicationFacade.RejectApplication(id, await EmployerFacade.GetEmployerByEmail(User.Identity.Name));
            return RedirectToAction("ApplicationsByJobOffer", new { id = (await JobApplicationFacade.GetApplication(id)).JobOffer.Id });
        }

        // GET: JobApplication/RejectApplication/5
        public async Task<ActionResult> CloseApplication(int id)
        {
            ViewBag.Action = "Close application";

            var emoloyer = await EmployerFacade.GetEmployerByEmail(User.Identity.Name);
            var application = await JobApplicationFacade.GetApplication(id);

            if (application.JobOffer.Employer.Id != emoloyer.Id)
                throw new ArgumentException();

            return View("ChangeApplicationStatus", application);
        }

        // POST: JobApplication/RejectApplication/5
        [HttpPost]
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> CloseApplication(int id, FormCollection collection)
        {
            await JobApplicationFacade.CloseApplication(id, await EmployerFacade.GetEmployerByEmail(User.Identity.Name));
            return RedirectToAction("ApplicationsByJobOffer", new { id = (await JobApplicationFacade.GetApplication(id)).JobOffer.Id });
        }
    }
}