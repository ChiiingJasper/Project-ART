using Microsoft.AspNetCore.Mvc;
using Project_ART.Data;
using Project_ART.Models;

namespace Project_ART.Controllers
{
    public class ToolController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ToolController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {

            var id = HttpContext.Session.GetInt32("Id");
            if(id == null)
            {
                return RedirectToAction("","", null);
            }
            var PendingCount = _db.Status
            .Where(o => o.Status == "Pending" && o.Is_Deleted == false)
            .Count();

            ViewBag.PendingCount = PendingCount;

            var ApprovedCount = _db.Status
            .Where(o => o.Status == "Approved" && o.Is_Deleted == false)
            .Count();

            ViewBag.ApprovedCount = ApprovedCount;

            var HiredCount = _db.Status
            .Where(o => o.Status == "Hired" && o.Is_Deleted == false)
            .Count();

            ViewBag.HiredCount = HiredCount;

            var AssessedCount = _db.Status
            .Where(o => o.Assessed_By != null)
            .Count();

            ViewBag.AssessedCount = AssessedCount;
            return View();
        }

       
    }
}
