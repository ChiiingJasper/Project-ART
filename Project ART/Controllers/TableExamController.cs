using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Mvc;

namespace Project_ART.Controllers
{
    public class TableExamController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TableExamController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<TableExam> objTableExamList = _db.Exam;
            return View(objTableExamList);
        }

        /*
        public IActionResult TableExam()
        {
            IEnumerable<TableExam> objTableExamList = _db.Exams;
            return View(objTableExamList);
        }
        */

        public IActionResult CreateExam()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateExam(TableExam obj)
        {
            _db.Exam.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("TableExam");
        }

        public IActionResult UpdateExam(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var examFromDb = _db.Exam.Find(id);

            if (examFromDb == null)
            {
                return NotFound();
            }

            return View(examFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateExam(TableExam obj)
        {
            _db.Exam.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("TableExam");

        }

        [HttpGet]
        public IActionResult DeleteExam(int? id)
        {
            var examFromDb = _db.Exam.Find(id);
            _db.Exam.Remove(examFromDb);
            _db.SaveChanges();
            return RedirectToAction("TableExam");
        }
    }
}
