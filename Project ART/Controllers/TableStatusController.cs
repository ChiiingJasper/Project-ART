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
    public class TableStatusController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TableStatusController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            dynamic obj = new ExpandoObject();
            bool deleteFlag = false;
            String tableName = "TableStatus";
            obj.Status = _db.Status.Where(x => x.Is_Deleted == deleteFlag);
            obj.Log = _db.Log.Where(x => x.Is_Deleted == deleteFlag && x.Table == tableName);

            return View(obj);
        }

        public IActionResult CreateStatus()
        {
            ViewBag.Candidate = GetCandidate();
            ViewBag.Approved = GetApproved();
            ViewBag.Assessed = GetAssessed();
            ViewBag.Hired = GetHired();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateStatus(TableStatus obj)
        {
            _db.Status.Add(obj);
            _db.SaveChanges();

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableStatus";
            String logDesc = "Created Status";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = obj.Status_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult UpdateStatus(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var statusFromDb = _db.Status.Find(id);

            if (statusFromDb == null)
            {
                return NotFound();
            }

            ViewBag.Candidate = GetCandidate();
            ViewBag.Approved = GetApproved();
            ViewBag.Assessed = GetAssessed();
            ViewBag.Hired = GetHired();
            return View(statusFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStatus(TableStatus obj)
        {
            _db.Status.Update(obj);
            _db.SaveChanges();

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableStatus";
            String logDesc = "Updated Status";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = obj.Status_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult DeleteStatus(int? id)
        {
            bool statusFlag = true;
            var statusFromDb = _db.Status.Find(id);
            var status = new TableStatus() { Status_ID = statusFromDb.Status_ID, Is_Deleted = statusFlag };

            _db.Status.Attach(status);
            _db.Entry(status).Property(x => x.Is_Deleted).IsModified = true;
            _db.SaveChanges();

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableStatus";
            String logDesc = "Deleted Status";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = statusFromDb.Status_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //TO GET LISTS
        private List<SelectListItem> GetCandidate()
        {
            var lstCandidates = new List<SelectListItem>();
            foreach (var item in _db.Candidate)
            {
                lstCandidates.Add(new SelectListItem()
                {
                    Value = item.Candidate_ID.ToString(),
                    Text = item.First_Name
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Candidate----"

            };

            lstCandidates.Insert(0, defItem);

            return lstCandidates;
        }

        private List<SelectListItem> GetApproved()
        {
            var lstApplications = new List<SelectListItem>();
            foreach (var item in _db.User)
            {
                lstApplications.Add(new SelectListItem()
                {
                    Value = item.Company_ID.ToString(),
                    Text = item.First_Name
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select User who Approved----"

            };

            lstApplications.Insert(0, defItem);

            return lstApplications;
        }

        private List<SelectListItem> GetAssessed()
        {
            var lstAssessments = new List<SelectListItem>();
            foreach (var item in _db.User)
            {
                lstAssessments.Add(new SelectListItem()
                {
                    Value = item.Company_ID.ToString(),
                    Text = item.First_Name
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select User who Assessed----"

            };

            lstAssessments.Insert(0, defItem);

            return lstAssessments;
        }

        private List<SelectListItem> GetHired()
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
                Text = "----Select User who Hired----"

            };

            lstUsers.Insert(0, defItem);

            return lstUsers;
        }
    }
}
