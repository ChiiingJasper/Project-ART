using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_ART.Controllers
{
    public class TableAssessmentController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TableAssessmentController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<TableAssessment> objTableAssessmentList = _db.Assessment;
            return View(objTableAssessmentList);
        }

        public IActionResult CreateAssessment()
        {
            ViewBag.Exams = GetExams();
            ViewBag.Interviews = GetInterviews();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAssessment(TableAssessment obj)
        {
            _db.Assessment.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateAssessment(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var assessmentFromDb = _db.Assessment.Find(id);

            if (assessmentFromDb == null)
            {
                return NotFound();
            }

            ViewBag.Exams = GetExams();
            ViewBag.Interviews = GetInterviews();
            return View(assessmentFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateAssessment(TableAssessment obj)
        {
            _db.Assessment.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult DeleteAssessment(int? id)
        {
            bool assessFlag = true;
            var assessFromDb = _db.Assessment.Find(id);
            var assessment = new TableAssessment() { Assessment_ID = assessFromDb.Assessment_ID, Is_Deleted = assessFlag };

            _db.Assessment.Attach(assessment);
            _db.Entry(assessment).Property(x => x.Is_Deleted).IsModified = true;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        //TO GET LISTS
        private List<SelectListItem> GetExams()
        {
            var lstExams = new List<SelectListItem>();
            foreach (var item in _db.Exam)
            {
                lstExams.Add(new SelectListItem()
                {
                    Value = item.Exam_ID.ToString(),
                    Text = item.Exam_Score.ToString()
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Exam----"

            };

            lstExams.Insert(0, defItem);

            return lstExams;
        }

        private List<SelectListItem> GetInterviews()
        {
            var lstInterview = new List<SelectListItem>();
            foreach (var item in _db.Interview)
            {
                lstInterview.Add(new SelectListItem()
                {
                    Value = item.Interview_ID.ToString(),
                    Text = item.Interview_Score.ToString()
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Interview----"

            };

            lstInterview.Insert(0, defItem);

            return lstInterview;
        }

    }
}
