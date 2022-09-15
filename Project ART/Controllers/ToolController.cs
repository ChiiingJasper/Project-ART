using Microsoft.AspNetCore.Mvc;

namespace Project_ART.Controllers
{
    public class ToolController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
