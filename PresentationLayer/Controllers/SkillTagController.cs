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

        // GET: SkillTag/Details/5
        public async Task<ActionResult> Details(int id)
        {
            SkillTagDto skills = await SkillFacade.GetSkill(id);
            return View(skills);
        }

        // GET: SkillTag/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SkillTag/Create
        [HttpPost]
        public async Task<ActionResult> Create(SkillTagDto dto)
        {
            if (ModelState.IsValid)
            {
                await SkillFacade.CreateSkill(dto);
                return RedirectToAction("Index");
            }

            return View(dto);
        }

        // GET: SkillTag/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var skill = await SkillFacade.GetSkill(id);
            return View(skill);
        }

        // POST: SkillTag/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, SkillTagDto dto)
        {
            if (ModelState.IsValid)
            {
                await SkillFacade.EditSkill(dto);
                return RedirectToAction("Index");
            }

            return View(dto);
        }

        // GET: SkillTag/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SkillTag/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, SkillTagDto dto)
        {
            try
            {
                await SkillFacade.DeleteSkill(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}