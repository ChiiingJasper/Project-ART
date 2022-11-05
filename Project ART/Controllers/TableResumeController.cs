using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
using System.Dynamic;
using System.Net;
using System.Net.Mail;


namespace Project_ART.Controllers
{
    public class TableResumeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TableResumeController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;

        }
        
        public IActionResult Index()
        {
            dynamic obj = new ExpandoObject();
            bool deleteFlag = false;
            String tableName = "TableResume";
            obj.Resume = _db.Resume.Where(x => x.Is_Deleted == deleteFlag);
            obj.Log = _db.Log.Where(x => x.Is_Deleted == deleteFlag && x.Table == tableName);

            return View(obj);
        }

        public IActionResult CreateResume()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateResume(TableResume obj)
        {
            _db.Resume.Add(obj);
            _db.SaveChanges();

            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableResume";
            String logDesc = "Created Resume";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = obj.Resume_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult UpdateResume(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var resumeFromDb = _db.Resume.Find(id);

            if (resumeFromDb == null)
            {
                return NotFound();
            }
            return View(resumeFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateResume(TableResume obj)
        {
            if (ModelState.IsValid)
            {
                _db.Resume.Update(obj);
                _db.SaveChanges();

                int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
                DateTime now = DateTime.Now;
                String tableName = "TableResume";
                String logDesc = "Updated Resume";
                TableLog tableLog = new TableLog();
                tableLog.Table = tableName;
                tableLog.Table_ID = obj.Resume_ID;
                tableLog.Description = logDesc;
                tableLog.Date_Time = now.ToString("F");
                tableLog.User_ID = (int)SessionId;
                _db.Log.Add(tableLog);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        [HttpGet]
        public IActionResult DeleteResume(int? id)
        {

            bool resumeFlag = true;
            var resumeFromDb = _db.Resume.Find(id);
            var resume = new TableResume() { Resume_ID = resumeFromDb.Resume_ID, Is_Deleted = resumeFlag };
           
            _db.Resume.Attach(resume);
            _db.Entry(resume).Property(x => x.Is_Deleted).IsModified = true;
            _db.SaveChanges();

            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableResume";
            String logDesc = "Deleted Resume";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = resumeFromDb.Resume_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
