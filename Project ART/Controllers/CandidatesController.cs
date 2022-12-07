using Microsoft.AspNetCore.Mvc;
using Project_ART.Data;
using Project_ART.Models;
using System.Dynamic;

namespace Project_ART.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CandidatesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            bool deleteFlag = false;
            String tableName = "TableCandidate";
            IEnumerable<TableCandidate> obj = _db.Candidate.Where(x => x.Is_Deleted == deleteFlag);
            
            return View(obj);
        }

        public IActionResult UploadCandidate()
        {
            

            return View();
        }
    }
}
