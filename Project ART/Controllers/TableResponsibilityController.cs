using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_ART.Controllers
{
    public class TableResponsibilityController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TableResponsibilityController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<TableResponsibility> objTableResponsibilityList = _db.Responsibility;
            return View(objTableResponsibilityList);
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
