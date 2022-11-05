using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Matching;
using System.Dynamic;
using System.Net;
using System.Net.Mail;

namespace Project_ART.Controllers
{
    public class TableAssessmentController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TableAssessmentController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            dynamic obj = new ExpandoObject();
            bool deleteFlag = false;
            String tableName = "TableAssessment";
            obj.Assessment = _db.Assessment.Where(x => x.Is_Deleted == deleteFlag);
            obj.Log = _db.Log.Where(x => x.Is_Deleted == deleteFlag && x.Table == tableName);

            return View(obj);
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

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableAssessment";
            String logDesc = "Created Assessment";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = obj.Assessment_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
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

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableAssessment";
            String logDesc = "Updated Assessment";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = obj.Assessment_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
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

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableAssessment";
            String logDesc = "Deleted Assessment";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = assessFromDb.Assessment_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
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
