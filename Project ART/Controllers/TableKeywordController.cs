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
    public class TableKeywordController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TableKeywordController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            dynamic obj = new ExpandoObject();
            bool deleteFlag = false;
            String tableName = "TableKeyword";
            obj.Keyword = _db.KeyWord.Where(x => x.Is_Deleted == deleteFlag);
            obj.Log = _db.Log.Where(x => x.Is_Deleted == deleteFlag && x.Table == tableName);

            return View(obj);
        }


        public IActionResult CreateKeyword()
        {
            ViewBag.Introductions = GetIntroductions();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateKeyword(TableKeyword obj)
        {
            _db.KeyWord.Add(obj);
            _db.SaveChanges();

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableKeyword";
            String logDesc = "Created Keyword";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = obj.Key_Word_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateKeyword(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var keywordFromDb = _db.KeyWord.Find(id);

            if (keywordFromDb == null)
            {
                return NotFound();
            }

            ViewBag.Introductions = GetIntroductions();
            return View(keywordFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateKeyword(TableKeyword obj)
        {
            _db.KeyWord.Update(obj);
            _db.SaveChanges();

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableKeyword";
            String logDesc = "Updated Keyword";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = obj.Key_Word_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult DeleteKeyword(int? id)
        {

            bool keyFlag = true;
            var keyFromDb = _db.KeyWord.Find(id);
            var keyword = new TableKeyword() { Key_Word_ID = keyFromDb.Key_Word_ID, Is_Deleted = keyFlag };

            _db.KeyWord.Attach(keyword);
            _db.Entry(keyword).Property(x => x.Is_Deleted).IsModified = true;
            _db.SaveChanges();

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableKeyword";
            String logDesc = "Deleted Keyword";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = keyFromDb.Key_Word_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        private List<SelectListItem> GetIntroductions()
        {
            var lstIntroductions = new List<SelectListItem>();
            foreach (var item in _db.Introduction)
            {
                lstIntroductions.Add(new SelectListItem()
                {
                    Value = item.Introduction_ID.ToString(),
                    Text = item.Introduction_Video
                    
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Introduction----"

            };

            lstIntroductions.Insert(0, defItem);

            return lstIntroductions;
        }
    }
}
