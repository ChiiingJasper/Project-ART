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
            IEnumerable<TableIntroduction> objTableIntroductionList = _db.Introduction;
            return View(objTableIntroductionList);
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
            _db.Introduction.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateIntroduction(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var introFromDb = _db.Introduction.Find(id);

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
            _db.Introduction.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult DeleteIntroduction(int? id)
        {
            bool introductionFlag = true;
            var introFromDb = _db.Introduction.Find(id);
            var introduction = new TableIntroduction() { Introduction_ID = introFromDb.Introduction_ID, Is_Deleted = introductionFlag };

            _db.Introduction.Attach(introduction);
            _db.Entry(introduction).Property(x => x.Is_Deleted).IsModified = true;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
