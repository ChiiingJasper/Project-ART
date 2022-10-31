using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_ART.Controllers
{
    public class TableIntroductionController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TableIntroductionController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        /*
        public IActionResult TableIntroduction()
        {
            IEnumerable<TableIntroduction> objTableIntroductionList = _db.Introductions;
            return View(objTableIntroductionList);
        }
        */

        public IActionResult CreateIntroduction()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateIntroduction(TableIntroduction obj)
        {
            _db.Introductions.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("TableIntroduction");
        }

        public IActionResult UpdateIntroduction(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var introFromDb = _db.Introductions.Find(id);

            if (introFromDb == null)
            {
                return NotFound();
            }


            return View(introFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateIntroduction(TableIntroduction obj)
        {
            _db.Introductions.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("TableIntroduction");

        }

        [HttpGet]
        public IActionResult DeleteIntroduction(int? id)
        {
            var introFromDb = _db.Introductions.Find(id);
            _db.Introductions.Remove(introFromDb);
            _db.SaveChanges();
            return RedirectToAction("TableIntroduction");
        }
    }
}
