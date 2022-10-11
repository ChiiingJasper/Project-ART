using Microsoft.AspNetCore.Mvc;
using Project_ART.Data;
using Project_ART.Models;
using System.Diagnostics;

namespace Project_ART.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginModel credentials)
        {
            var user = _db.Users.SingleOrDefault(x => x.Email == credentials.Email);

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(credentials.Password, user.Password);
            

            if(isValidPassword)
            {
                TableUser u = _db.Users.FirstOrDefault(x => x.Email == credentials.Email && x.Password == credentials.Password);
                string str = System.Convert.ToString(isValidPassword);
                return RedirectToAction("Index", "Tool");
            }
            else
            {
                return Content("JEff");
            }
            
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}