using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_ART.Controllers
{
    public class TableJobApplicationController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TableJobApplicationController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<TableJobApplication> objTableJobApplicationList = _db.JobApplication;
            return View(objTableJobApplicationList);
        }

        public IActionResult CreateJobApplication()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateJobApplication(TableJobApplication obj)
        {
            _db.JobApplication.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateJobApplication(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var jobAppFromDb = _db.JobApplication.Find(id);

            if (jobAppFromDb == null)
            {
                return NotFound();
            }
            return View(jobAppFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateJobApplication(TableJobApplication obj)
        {
            _db.JobApplication.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult DeleteJobApplication(int? id)
        {
            bool jobAppFlag = true;
            var jobAppFromDb = _db.JobApplication.Find(id);
            var jobApplication = new TableJobApplication() { Job_Application_ID = jobAppFromDb.Job_Application_ID, Is_Deleted = jobAppFlag };

            _db.JobApplication.Attach(jobApplication);
            _db.Entry(jobApplication).Property(x => x.Is_Deleted).IsModified = true;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
