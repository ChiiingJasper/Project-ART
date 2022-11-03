using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Project_ART.Controllers
{
    public class TableResumeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public TableResumeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<TableResume> objTableResumeList = _db.Resume;
            return View(objTableResumeList);
        }

        public IActionResult CreateResume()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateResume(TableResume obj)
        {
            _db.Resume.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateResume(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var resumeFromDb = _db.Resume.Find(id);

            if (resumeFromDb == null)
            {
                return NotFound();
            }
            return View(resumeFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateResume(TableResume obj)
        {
            if (ModelState.IsValid)
            {
                _db.Resume.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        [HttpGet]
        public IActionResult DeleteResume(int? id)
        {

            bool resumeFlag = true;
            var resumeFromDb = _db.Resume.Find(id);
            var resume = new TableResume() { Resume_ID = resumeFromDb.Resume_ID, Is_Deleted = resumeFlag };
           
            _db.Resume.Attach(resume);
            _db.Entry(resume).Property(x => x.Is_Deleted).IsModified = true;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
