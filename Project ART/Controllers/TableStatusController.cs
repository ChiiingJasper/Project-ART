using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_ART.Controllers
{
    public class TableStatusController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TableStatusController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<TableStatus> objTableStatusList = _db.Status;
            return View(objTableStatusList);
        }


        public IActionResult UpdateStatus(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var statusFromDb = _db.Status.Find(id);

            if (statusFromDb == null)
            {
                return NotFound();
            }

            ViewBag.Candidate = GetCandidate();
            ViewBag.Approved = GetApproved();
            ViewBag.Assessed = GetAssessed();
            ViewBag.Hired = GetHired();
            return View(statusFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStatus(TableStatus obj)
        {
            _db.Status.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult DeleteStatus(int? id)
        {
            bool statusFlag = true;
            var statusFromDb = _db.Status.Find(id);
            var status = new TableStatus() { Status_ID = statusFromDb.Status_ID, Is_Deleted = statusFlag };

            _db.Status.Attach(status);
            _db.Entry(status).Property(x => x.Is_Deleted).IsModified = true;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        //TO GET LISTS
        private List<SelectListItem> GetCandidate()
        {
            var lstApplications = new List<SelectListItem>();
            foreach (var item in _db.Candidate)
            {
                lstApplications.Add(new SelectListItem()
                {
                    Value = item.Candidate_ID.ToString(),
                    Text = item.First_Name
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Candidate----"

            };

            lstApplications.Insert(0, defItem);

            return lstApplications;
        }

        private List<SelectListItem> GetApproved()
        {
            var lstApplications = new List<SelectListItem>();
            foreach (var item in _db.User)
            {
                lstApplications.Add(new SelectListItem()
                {
                    Value = item.Company_ID.ToString(),
                    Text = item.First_Name
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select User who Approved----"

            };

            lstApplications.Insert(0, defItem);

            return lstApplications;
        }

        private List<SelectListItem> GetAssessed()
        {
            var lstAssessments = new List<SelectListItem>();
            foreach (var item in _db.User)
            {
                lstAssessments.Add(new SelectListItem()
                {
                    Value = item.Company_ID.ToString(),
                    Text = item.First_Name
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select User who Assessed----"

            };

            lstAssessments.Insert(0, defItem);

            return lstAssessments;
        }

        private List<SelectListItem> GetHired()
        {
            var lstUsers = new List<SelectListItem>();
            foreach (var item in _db.User)
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
                Text = "----Select User who Hired----"

            };

            lstUsers.Insert(0, defItem);

            return lstUsers;
        }
    }
}
