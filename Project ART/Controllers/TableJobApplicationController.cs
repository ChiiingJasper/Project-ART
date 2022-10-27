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

        /*
        public IActionResult TableJobApplication()
        {
            IEnumerable<TableJobApplication> objTableJobApplicationList = _db.JobApplication;
            return View(objTableJobApplicationList);
        }
        */

        public IActionResult CreateJobApplication()
        {
            ViewBag.Datasheets = GetDatasheets();
            ViewBag.Introductions = GetIntroductions();
            ViewBag.Users = GetUsers();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateJobApplication(TableJobApplication obj)
        {
            _db.JobApplication.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("TableJobApplication");
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

            ViewBag.Datasheets = GetDatasheets();
            ViewBag.Introductions = GetIntroductions();
            ViewBag.Users = GetUsers();
            return View(jobAppFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateJobApplication(TableJobApplication obj)
        {
            _db.JobApplication.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("TableJobApplication");

        }

        [HttpGet]
        public IActionResult DeleteJobApplication(int? id)
        {
            var jobAppFromDb = _db.JobApplication.Find(id);
            _db.JobApplication.Remove(jobAppFromDb);
            _db.SaveChanges();
            return RedirectToAction("TableJobApplication");
        }

        private List<SelectListItem> GetDatasheets()
        {
            var lstDatasheets = new List<SelectListItem>();
            foreach (var item in _db.Datasheets)
            {
                lstDatasheets.Add(new SelectListItem()
                {
                    Value = item.Data_Sheet_ID.ToString(),
                    Text = item.First_Name
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Datasheet----"

            };

            lstDatasheets.Insert(0, defItem);

            return lstDatasheets;
        }

        private List<SelectListItem> GetIntroductions()
        {
            var lstIntroductions = new List<SelectListItem>();
            foreach (var item in _db.Introductions)
            {
                lstIntroductions.Add(new SelectListItem()
                {
                    Value = item.Introduction_ID.ToString(),
                    Text = item.B5_Trait
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

        private List<SelectListItem> GetUsers()
        {
            var lstUsers = new List<SelectListItem>();
            foreach (var item in _db.Users)
            {
                lstUsers.Add(new SelectListItem()
                {
                    Value = item.Company_ID.ToString(),
                    Text = item.First_Name
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select User----"

            };

            lstUsers.Insert(0, defItem);

            return lstUsers;
        }
    }
}
