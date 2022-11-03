using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Mvc;

namespace Project_ART.Controllers
{
    public class TableInterviewController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TableInterviewController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<TableInterview> objTableInterviewList = _db.Interview;
            return View(objTableInterviewList);
        }


        public IActionResult CreateInterview()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateInterview(TableInterview obj)
        {
            _db.Interview.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateInterview(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var interviewFromDb = _db.Interview.Find(id);

            if (interviewFromDb == null)
            {
                return NotFound();
            }

            return View(interviewFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateInterview(TableInterview obj)
        {
            _db.Interview.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult DeleteInterview(int? id)
        {
            bool interviewFlag = true;
            var interviewFromDb = _db.Interview.Find(id);
            var interview = new TableInterview() { Interview_ID = interviewFromDb.Interview_ID, Is_Deleted = interviewFlag };

            _db.Interview.Attach(interview);
            _db.Entry(interview).Property(x => x.Is_Deleted).IsModified = true;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
