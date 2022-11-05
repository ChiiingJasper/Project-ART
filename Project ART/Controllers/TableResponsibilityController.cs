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
    public class TableResponsibilityController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TableResponsibilityController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            dynamic obj = new ExpandoObject();
            bool deleteFlag = false;
            String tableName = "TableResponsibility";
            obj.Responsibility = _db.Responsibility.Where(x => x.Is_Deleted == deleteFlag);
            obj.Log = _db.Log.Where(x => x.Is_Deleted == deleteFlag && x.Table == tableName);

            return View(obj);
        }

        public IActionResult CreateResponsibility()
        {
            ViewBag.Application = GetJobApplication();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateResponsibility(TableResponsibility obj)
        {
            TableResponsibility respoTable = new TableResponsibility();
            if (obj.Job_Application_ID != null)
            {
                respoTable.Job_Application_ID = obj.Job_Application_ID;
            }
            respoTable.Responsibility = obj.Responsibility;
            respoTable.Description = obj.Description;
            _db.Responsibility.Add(respoTable);
            _db.SaveChanges();

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableResponsibility";
            String logDesc = "Created Responsibility";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = respoTable.Responsibility_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateResponsibility(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var respoFromDb = _db.Responsibility.Find(id);

            if (respoFromDb == null)
            {
                return NotFound();
            }

            ViewBag.JobApplication = GetJobApplication();
            return View(respoFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateResponsibility(TableResponsibility obj)
        {
            _db.Responsibility.Update(obj);
            _db.SaveChanges();

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableResponsibility";
            String logDesc = "Updated Responsibility";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = obj.Responsibility_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult DeleteResponsibility(int? id)
        {
            bool respoFlag = true;
            var respoFromDb = _db.Responsibility.Find(id);
            var responsibility = new TableResponsibility() { Responsibility_ID = respoFromDb.Responsibility_ID, Is_Deleted = respoFlag };

            _db.Responsibility.Attach(responsibility);
            _db.Entry(responsibility).Property(x => x.Is_Deleted).IsModified = true;
            _db.SaveChanges();

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableResponsibility";
            String logDesc = "Deleted Responsibility";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = respoFromDb.Responsibility_ID;
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
