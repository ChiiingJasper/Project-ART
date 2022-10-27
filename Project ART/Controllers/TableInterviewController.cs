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
            IEnumerable<TableInterview> objTableInterviewList = _db.Interviews;
            return View(objTableInterviewList);
        }

        /*
        public IActionResult TableInterview()
        {
            IEnumerable<TableInterview> objTableInterviewList = _db.Interviews;
            return View(objTableInterviewList);
        }
        */

        public IActionResult CreateInterview()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateInterview(TableInterview obj)
        {
            _db.Interviews.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("TableInterview");
        }

        public IActionResult UpdateInterview(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var interviewFromDb = _db.Interviews.Find(id);

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
            _db.Interviews.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("TableInterview");

        }

        [HttpGet]
        public IActionResult DeleteInterview(int? id)
        {
            var interviewFromDb = _db.Interviews.Find(id);
            _db.Interviews.Remove(interviewFromDb);
            _db.SaveChanges();
            return RedirectToAction("TableInterview");
        }
    }
}
