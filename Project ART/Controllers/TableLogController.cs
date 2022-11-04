using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_ART.Controllers
{
    public class TableLogController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TableLogController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<TableLog> objTableLogList = _db.Log;
            return View(objTableLogList);
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
