using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_ART.Controllers
{
    public class TableBenefitController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TableBenefitController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<TableBenefit> objTableBenefitList = _db.Benefit;
            return View(objTableBenefitList);
        }

        public IActionResult CreateBenefit()
        {
            ViewBag.Application = GetJobApplication();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBenefit(TableBenefit obj)
        {
            TableBenefit benefitTable = new TableBenefit();
            if(obj.Job_Application_ID != null)
            {
                benefitTable.Job_Application_ID = obj.Job_Application_ID;
            }
            benefitTable.Benefit = obj.Benefit;
            benefitTable.Description = obj.Description;
            _db.Benefit.Add(benefitTable);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        public IActionResult UpdateBenefit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var benefitFromDb = _db.Benefit.Find(id);

            if (benefitFromDb == null)
            {
                return NotFound();
            }

            ViewBag.JobApplication = GetJobApplication();
            return View(benefitFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateBenefit(TableBenefit obj)
        {
            _db.Benefit.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult DeleteBenefit(int? id)
        {
            bool benefitFlag = true;
            var benefitFromDb = _db.Benefit.Find(id);
            var benefit = new TableBenefit() { Benefit_ID = benefitFromDb.Benefit_ID, Is_Deleted = benefitFlag };

            _db.Benefit.Attach(benefit);
            _db.Entry(benefit).Property(x => x.Is_Deleted).IsModified = true;
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
