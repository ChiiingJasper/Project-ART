using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_ART.Controllers
{
    public class TableQualificationController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TableQualificationController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<TableQualification> objTableQualificationList = _db.Qualification;
            return View(objTableQualificationList);
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
