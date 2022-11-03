using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Mvc;

namespace Project_ART.Controllers
{
    public class RankingsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public RankingsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<TableCandidate> obj = _db.Candidate;
            return View(obj);
        }
    }
}
