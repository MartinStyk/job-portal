using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Facades;

namespace PresentationLayer.Controllers
{
    public class JobOfferController : Controller
    {
        public JobOfferFacade JobOfferFacade { get; set; }

        // GET: JobOffer
        public async Task<ActionResult> Index()
        {
            var offers = (await JobOfferFacade.GetAllOffers()).Items;
            return View(offers);
        }

        // GET: JobOffer/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var offer = await JobOfferFacade.GetOffer(id);
            return View(offer);
        }

        // GET: JobOffer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobOffer/Create
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

        // GET: JobOffer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: JobOffer/Edit/5
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