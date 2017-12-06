using System.Threading.Tasks;
using System.Web.Mvc;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades;

namespace PresentationLayer.Controllers
{
    public class EmployerController : Controller
    {
        public EmployerFacade EmployerFacade { get; set; }


        // GET: Employer
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var employers = (await EmployerFacade.GetAllEmployersAsync()).Items;
            return View(employers);
        }

        // GET: Employer/Details/5
        [AllowAnonymous]
        public async Task<ActionResult> Details(int id)
        {
            var employers = await EmployerFacade.GetEmployerById(id);
            return View(employers);
        }

        // GET: Employer/EditCurrentEmployer
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> EditCurrentEmployer()
        {
            var user = await EmployerFacade.GetEmployerByEmail(User.Identity.Name);
            return View("Edit", user);
        }

        // POST: Employer/EditCurrentEmployer
        [Authorize(Roles = "Employer")]
        [HttpPost]
        public async Task<ActionResult> EditCurrentEmployer(EmployerDto employerDto)
        {
            if (ModelState.IsValid)
            {
                await EmployerFacade.Update(employerDto);
                return RedirectToAction("Index");
            }

            return View("Edit", employerDto);
        }
    }
}