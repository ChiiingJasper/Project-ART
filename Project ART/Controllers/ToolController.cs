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


            var Dominance =
                             from candidate in _db.Candidate.Where(x => x.Is_Deleted == false)
                             join status in _db.Status.Where(x => x.Is_Deleted == false && x.Status == "Hired") on candidate.Candidate_ID equals status.Candidate_ID
                             join introduction in _db.Introduction.Where(x => x.DISC_Trait == "Dominance") on candidate.Introduction_ID equals introduction.Introduction_ID
                             select new
                             {
                                 Candidate_ID = candidate.Candidate_ID,
                             };

            ViewBag.Dominance = Dominance.Count();

            var Influence =
                             from candidate in _db.Candidate.Where(x => x.Is_Deleted == false)
                             join status in _db.Status.Where(x => x.Is_Deleted == false && x.Status == "Hired") on candidate.Candidate_ID equals status.Candidate_ID
                             join introduction in _db.Introduction.Where(x => x.DISC_Trait == "Influence") on candidate.Introduction_ID equals introduction.Introduction_ID
                             select new
                             {
                                 Candidate_ID = candidate.Candidate_ID,
                             };

            ViewBag.Influence = Influence.Count();

            var Steadiness =
                             from candidate in _db.Candidate.Where(x => x.Is_Deleted == false)
                             join status in _db.Status.Where(x => x.Is_Deleted == false && x.Status == "Hired") on candidate.Candidate_ID equals status.Candidate_ID
                             join introduction in _db.Introduction.Where(x => x.DISC_Trait == "Steadiness") on candidate.Introduction_ID equals introduction.Introduction_ID
                             select new
                             {
                                 Candidate_ID = candidate.Candidate_ID,
                             };

            ViewBag.Steadiness = Steadiness.Count();

            var Compliance =
                             from candidate in _db.Candidate.Where(x => x.Is_Deleted == false)
                             join status in _db.Status.Where(x => x.Is_Deleted == false && x.Status == "Hired") on candidate.Candidate_ID equals status.Candidate_ID
                             join introduction in _db.Introduction.Where(x => x.DISC_Trait == "Compliance") on candidate.Introduction_ID equals introduction.Introduction_ID
                             select new
                             {
                                 Candidate_ID = candidate.Candidate_ID,
                             };

            ViewBag.Compliance = Compliance.Count();

            ViewBag.Candidates = _db.Candidate.Where(x => x.Is_Deleted == false).Count();

            return View();
        }

       
    }
}
