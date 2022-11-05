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
    public class TableLogController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TableLogController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            dynamic obj = new ExpandoObject();
            bool deleteFlag = false;
            String tableName = "TableLog";
            obj.Logs = _db.Log.Where(x => x.Is_Deleted == deleteFlag);
            obj.Log = _db.Log.Where(x => x.Is_Deleted == deleteFlag && x.Table == tableName);

            return View(obj);
        }
        public IActionResult CreateLog()
        {
            ViewBag.Updated = GetUpdated();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateLog(TableLog obj)
        {
            _db.Log.Add(obj);
            _db.SaveChanges();

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableLog";
            String logDesc = "Created Log";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = obj.Log_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateLog(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var logFromDb = _db.Log.Find(id);

            if (logFromDb == null)
            {
                return NotFound();
            }

            ViewBag.Updated = GetUpdated();
            return View(logFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateLog(TableLog obj)
        {
            _db.Log.Update(obj);
            _db.SaveChanges();

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableLog";
            String logDesc = "Updated Log";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = obj.Log_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult DeleteLog(int? id)
        {

            bool logFlag = true;
            var logFromDb = _db.Log.Find(id);
            var log = new TableLog() { Log_ID = logFromDb.Log_ID, Is_Deleted = logFlag };

            _db.Log.Attach(log);
            _db.Entry(log).Property(x => x.Is_Deleted).IsModified = true;
            _db.SaveChanges();

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableLog";
            String logDesc = "Deleted Log";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = logFromDb.Log_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        private List<SelectListItem> GetUpdated()
        {
            var lstUser = new List<SelectListItem>();
            foreach (var item in _db.User)
            {
                lstUser.Add(new SelectListItem()
                {
                    Value = item.Company_ID.ToString(),
                    Text = item.First_Name
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select User who Updated----"

            };

            lstUser.Insert(0, defItem);

            return lstUser;
        }
    }
}
