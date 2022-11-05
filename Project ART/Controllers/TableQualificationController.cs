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
    public class TableQualificationController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TableQualificationController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            dynamic obj = new ExpandoObject();
            bool deleteFlag = false;
            String tableName = "TableQualification";
            obj.Qualification = _db.Qualification.Where(x => x.Is_Deleted == deleteFlag);
            obj.Log = _db.Log.Where(x => x.Is_Deleted == deleteFlag && x.Table == tableName);

            return View(obj);
        }

        public IActionResult CreateQualification()
        {
            ViewBag.Application = GetJobApplication();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateQualification(TableQualification obj)
        {
            TableQualification qualiTable = new TableQualification();
            if (obj.Job_Application_ID != null)
            {
                qualiTable.Job_Application_ID = obj.Job_Application_ID;
            }
            qualiTable.Qualification = obj.Qualification;
            qualiTable.Description = obj.Description;
            _db.Qualification.Add(qualiTable);
            _db.SaveChanges();

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableQualification";
            String logDesc = "Created Qualification";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = qualiTable.Qualification_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult UpdateQualification(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var qualiFromDb = _db.Qualification.Find(id);

            if (qualiFromDb == null)
            {
                return NotFound();
            }

            ViewBag.JobApplication = GetJobApplication();
            return View(qualiFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateQualification(TableQualification obj)
        {
            _db.Qualification.Update(obj);
            _db.SaveChanges();

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableQualification";
            String logDesc = "Updated Qualification";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = obj.Qualification_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult DeleteQualification(int? id)
        {
            bool qualiFlag = true;
            var qualiFromDb = _db.Qualification.Find(id);
            var qualification = new TableQualification() { Qualification_ID = qualiFromDb.Qualification_ID, Is_Deleted = qualiFlag };

            _db.Qualification.Attach(qualification);
            _db.Entry(qualification).Property(x => x.Is_Deleted).IsModified = true;
            _db.SaveChanges();

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableQualification";
            String logDesc = "Deleted Qualification";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = qualiFromDb.Qualification_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //TO GET LISTS
        private List<SelectListItem> GetJobApplication()
        {
            var lstJobApp = new List<SelectListItem>();
            foreach (var item in _db.JobApplication)
            {
                lstJobApp.Add(new SelectListItem()
                {
                    Value = item.Job_Application_ID.ToString(),
                    Text = item.Job.ToString()
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Job Application----"

            };

            lstJobApp.Insert(0, defItem);

            return lstJobApp;
        }

    }
}
