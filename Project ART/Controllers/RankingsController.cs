using Microsoft.AspNetCore.Mvc;

namespace Project_ART.Controllers
{
    public class RankingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
