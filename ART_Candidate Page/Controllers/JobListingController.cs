using Microsoft.AspNetCore.Mvc;

namespace ART_Candidate_Page.Controllers
{
    public class JobListingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult JobDesc()
        {
            return View();
        }
    }
}
