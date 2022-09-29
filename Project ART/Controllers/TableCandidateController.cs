using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_ART.Controllers
{
    public class TableCandidateController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TableCandidateController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TableCandidate()
        {
            IEnumerable<TableCandidate> objTableCandidateList = _db.Candidates;
            return View(objTableCandidateList);
        }

        public IActionResult CreateCandidate()
        {
            ViewBag.Applications = GetApplications();
            ViewBag.Assessments = GetAssessments();
            ViewBag.Users = GetUsers();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCandidate(TableCandidate obj)
        {
            _db.Candidates.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("TableCandidate");
        }

        public IActionResult UpdateCandidate(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var candidateFromDb = _db.Candidates.Find(id);

            if (candidateFromDb == null)
            {
                return NotFound();
            }

            ViewBag.Applications = GetApplications();
            ViewBag.Assessments = GetAssessments();
            ViewBag.Users = GetUsers();
            return View(candidateFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCandidate(TableCandidate obj)
        {
            _db.Candidates.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("TableCandidate");

        }

        [HttpGet]
        public IActionResult DeleteCandidate(int? id)
        {
            var candidateFromDb = _db.Candidates.Find(id);
            _db.Candidates.Remove(candidateFromDb);
            _db.SaveChanges();
            return RedirectToAction("TableCandidate");
        }

        //TO GET LISTS
        private List<SelectListItem> GetApplications()
        {
            var lstApplications = new List<SelectListItem>();
            foreach (var item in _db.JobApplication)
            {
                lstApplications.Add(new SelectListItem()
                {
                    Value = item.Application_ID.ToString(),
                    Text = item.Application_ID.ToString()
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Application----"

            };

            lstApplications.Insert(0, defItem);

            return lstApplications;
        }

        private List<SelectListItem> GetAssessments()
        {
            var lstAssessments = new List<SelectListItem>();
            foreach (var item in _db.Assessments)
            {
                lstAssessments.Add(new SelectListItem()
                {
                    Value = item.Assessment_ID.ToString(),
                    Text = item.Date_Assessed.ToString()
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Assessment----"

            };

            lstAssessments.Insert(0, defItem);

            return lstAssessments;
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
