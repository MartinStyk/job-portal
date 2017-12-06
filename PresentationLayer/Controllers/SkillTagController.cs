using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Facades;

namespace PresentationLayer.Controllers
{
    public class SkillTagController : Controller
    {
        public SkillFacade SkillFacade { get; set; }

        // GET: SkillTag
        public async Task<ActionResult> Index()
        {
            var skills = await SkillFacade.GetAllSkillsAsync();
            return View(skills.Items);
        }

      
        // GET: SkillTag/Create
        [Authorize(Roles = "Employer")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: SkillTag/Create
        [HttpPost]
        [Authorize(Roles = "Employer")]
        public async Task<ActionResult> Create(SkillTagDto dto)
        {
            if (ModelState.IsValid)
            {
                await SkillFacade.CreateSkill(dto);
                return RedirectToAction("Index");
            }

            return View(dto);
        }
    }
}