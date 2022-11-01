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
        /*
        public IActionResult TableAssessment()
        {
            IEnumerable<TableAssessment> objTableAssessmentList = _db.Assessments;
            return View(objTableAssessmentList);
        }
        */

        public IActionResult CreateAssessment()
        {
            ViewBag.Exams = GetExams();
            ViewBag.Interviews = GetInterviews();
            ViewBag.Users = GetUsers();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAssessment(TableAssessment obj)
        {
            _db.Assessment.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("TableAssessment");
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
            ViewBag.Users = GetUsers();
            return View(assessmentFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateAssessment(TableAssessment obj)
        {
            _db.Assessment.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("TableAssessment");

        }

        [HttpGet]
        public IActionResult DeleteAssessment(int? id)
        {
            var assessmentFromDb = _db.Assessment.Find(id);
            _db.Assessment.Remove(assessmentFromDb);
            _db.SaveChanges();
            return RedirectToAction("TableAssessment");
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

        private List<SelectListItem> GetUsers()
        {
            var lstUsers = new List<SelectListItem>();
            foreach (var item in _db.User)
            {
                lstUsers.Add(new SelectListItem()
                {
                    Value = item.Company_ID.ToString(),
                    Text = item.First_Name
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select User----"

            };

            lstUsers.Insert(0, defItem);

            return lstUsers;
        }
    }
}
