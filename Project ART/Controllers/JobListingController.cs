using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Dynamic;

namespace Project_ART.Controllers
{
    public class JobListingController : Controller
    {
        private readonly ApplicationDbContext _db;

        public JobListingController(ApplicationDbContext db)
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

        public IActionResult ViewJobApplication(int? id)
        {
            dynamic obj = new ExpandoObject();

            if (id == null || id == 0)
            {
                return NotFound();
            }
            obj.JobApplication = _db.JobApplication.Find(id);

            if (obj.JobApplication == null)
            {
                return NotFound();
            }

            obj.Responsibility = _db.Responsibility.Where(x => x.Job_Application_ID == id);
            obj.Qualification = _db.Qualification.Where(x => x.Job_Application_ID == id);
            obj.Benefit = _db.Benefit.Where(x => x.Job_Application_ID == id);

            return View(obj);
        }


    }
}
