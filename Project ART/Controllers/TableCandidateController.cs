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
    public class TableCandidateController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TableCandidateController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            dynamic obj = new ExpandoObject();
            bool deleteFlag = false;
            String tableName = "TableCandidate";
            obj.Candidate = _db.Candidate.Where(x => x.Is_Deleted == deleteFlag);
            obj.Log = _db.Log.Where(x => x.Is_Deleted == deleteFlag && x.Table == tableName);

            return View(obj);
        }

        public IActionResult CreateCandidate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCandidate(TableCandidate obj)
        {
            _db.Candidate.Add(obj);
            _db.SaveChanges();

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableCandidate";
            String logDesc = "Created Candidate";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = obj.Candidate_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateCandidate(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var candidateFromDb = _db.Candidate.Find(id);

            if (candidateFromDb == null)
            {
                return NotFound();
            }

            ViewBag.Applications = GetApplications();
            ViewBag.Assessments = GetAssessments();
            ViewBag.Resumes = GetResume();
            ViewBag.Introductions = GetIntroduction();
            return View(candidateFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCandidate(TableCandidate obj)
        {
            _db.Candidate.Update(obj);
            _db.SaveChanges();

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableCandidate";
            String logDesc = "Updated Candidate";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = obj.Candidate_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult DeleteCandidate(int? id)
        {
            bool candidateFlag = true;
            var candidateFromDb = _db.Candidate.Find(id);
            var candidate = new TableCandidate() { Candidate_ID = candidateFromDb.Candidate_ID, Is_Deleted = candidateFlag };

            _db.Candidate.Attach(candidate);
            _db.Entry(candidate).Property(x => x.Is_Deleted).IsModified = true;
            _db.SaveChanges();

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableCandidate";
            String logDesc = "Deleted Candidate";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = candidateFromDb.Candidate_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //TO GET LISTS
        private List<SelectListItem> GetApplications()
        {
            var lstApplications = new List<SelectListItem>();
            foreach (var item in _db.JobApplication)
            {
                lstApplications.Add(new SelectListItem()
                {
                    Value = item.Job_Application_ID.ToString(),
                    Text = item.Job_Application_ID.ToString()
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Application----"

            };

            lstApplications.Insert(0, defItem);

            return lstApplications;
        }

        private List<SelectListItem> GetAssessments()
        {
            var lstAssessments = new List<SelectListItem>();
            foreach (var item in _db.Assessment)
            {
                lstAssessments.Add(new SelectListItem()
                {
                    Value = item.Assessment_ID.ToString(),
                    //Text = item.Date_Assessed.ToString()
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Assessment----"

            };

            lstAssessments.Insert(0, defItem);

            return lstAssessments;
        }

        private List<SelectListItem> GetResume()
        {
            var lstResume = new List<SelectListItem>();
            foreach (var item in _db.Resume)
            {
                lstResume.Add(new SelectListItem()
                {
                    Value = item.Resume_ID.ToString(),
                    Text = item.Resume
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Resume----"

            };

            lstResume.Insert(0, defItem);

            return lstResume;
        }

        private List<SelectListItem> GetIntroduction()
        {
            var lstIntro = new List<SelectListItem>();
            foreach (var item in _db.Introduction)
            {
                lstIntro.Add(new SelectListItem()
                {
                    Value = item.Introduction_ID.ToString(),
                    Text = item.Introduction_Video
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Introduction Video----"

            };

            lstIntro.Insert(0, defItem);

            return lstIntro;
        }
    }
}
