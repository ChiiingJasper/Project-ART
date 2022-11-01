using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_ART.Controllers
{
    public class TableSkillController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TableSkillController(ApplicationDbContext db)
        {
            _db = db;
        }
        /*
        public IActionResult Index()
        {
            IEnumerable<TableSkill> objTableSkillList = _db.Skills;
            return View(objTableSkillList);
        }

        
        public IActionResult TableSkill()
        {
            IEnumerable<TableSkill> objTableSkillList = _db.Skills;
            return View(objTableSkillList);
        }
        */

        public IActionResult CreateSkill()
        {
            ViewBag.Datasheets = GetDatasheets();
            return View();
        }
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
          public IActionResult CreateSkill(TableSkill obj)
        {
            _db.Skills.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("TableSkill");
        }
        
        public IActionResult UpdateSkill(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var skillFromDb = _db.Skills.Find(id);

            if (skillFromDb == null)
            {
                return NotFound();
            }

            ViewBag.Datasheets = GetDatasheets();
            return View(skillFromDb);
        }
        */
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateSkill(TableSkill obj)
        {
            _db.Skills.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("TableSkill");

        }
        
        [HttpGet]
        public IActionResult DeleteSkill(int? id)
        {
            var skillFromDb = _db.Skills.Find(id);
            _db.Skills.Remove(skillFromDb);
            _db.SaveChanges();
            return RedirectToAction("TableSkill");
        }
        */
        private List<SelectListItem> GetDatasheets()
        {
            var lstDatasheets = new List<SelectListItem>();
           

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Datasheet----"

            };

            lstDatasheets.Insert(0, defItem);

            return lstDatasheets;
        }
    }
}
