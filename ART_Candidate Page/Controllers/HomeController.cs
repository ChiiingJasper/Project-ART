using ART_Candidate_Page.Data;
using ART_Candidate_Page.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace ART_Candidate_Page.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        
        public IActionResult EmailConfirm(int? id,String? hash)
        {
            if (id.HasValue && hash != null)
            {
                TableCandidate candidate = new TableCandidate();
                candidate = _db.Candidate.SingleOrDefault(x => x.Candidate_ID == id);
                if (candidate != null)
                {
                    bool isValidHash = BCrypt.Net.BCrypt.Verify(candidate.Candidate_ID+candidate.First_Name+candidate.Last_Name,hash);
                    if (isValidHash)
                    {
                        candidate.Email_Confirmed = true;
                        _db.Candidate.Update(candidate);
                        _db.SaveChanges();
                        return View();
                    }
                    
                }
                
            }
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}