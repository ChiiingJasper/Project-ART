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
    public class TableUserController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TableUserController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            dynamic obj = new ExpandoObject();
            bool deleteFlag = false;
            String tableName = "TableUser";
            obj.User = _db.User.Where(x => x.Is_Deleted == deleteFlag);
            obj.Log = _db.Log.Where(x => x.Is_Deleted == deleteFlag && x.Table == tableName);

            return View(obj);
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(TableUser obj)
        {
            obj.Password = BCrypt.Net.BCrypt.HashPassword(obj.Password);
            _db.User.Add(obj);
            _db.SaveChanges();

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableUser";
            String logDesc = "Created User";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = obj.Company_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        public IActionResult UpdateUser(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var userFromDb = _db.User.Find(id);

            if (userFromDb == null)
            {
                return NotFound();
            }
            return View(userFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateUser(TableUser obj)
        {
            if (ModelState.IsValid)
            {
                obj.Password = BCrypt.Net.BCrypt.HashPassword(obj.Password);
                _db.User.Update(obj);
                _db.SaveChanges();

                //ADD LOG
                int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
                DateTime now = DateTime.Now;
                String tableName = "TableUser";
                String logDesc = "Updated User";
                TableLog tableLog = new TableLog();
                tableLog.Table = tableName;
                tableLog.Table_ID = obj.Company_ID;
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
        public IActionResult DeleteUser(int? id)
        {
            bool userFlag = true;
            var userFromDb = _db.User.Find(id);
            var user = new TableUser() { Company_ID = userFromDb.Company_ID, Is_Deleted = userFlag };

            _db.User.Attach(user);
            _db.Entry(user).Property(x => x.Is_Deleted).IsModified = true;
            _db.SaveChanges();

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableUser";
            String logDesc = "Deleted User";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = userFromDb.Company_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
