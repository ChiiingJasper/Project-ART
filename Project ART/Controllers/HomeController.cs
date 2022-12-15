using Microsoft.AspNetCore.Mvc;
using Project_ART.Data;
using Project_ART.Models;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

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

            if (ModelState.IsValid)
            {
                var user = _db.User.SingleOrDefault(x => (x.Email == credentials.UserCred || Convert.ToString(x.Company_ID) == credentials.UserCred) && x.Is_Deleted == false);
                if(user != null)
                {
                    bool isValidPassword = BCrypt.Net.BCrypt.Verify(credentials.Password, user.Password);
                    if (isValidPassword)
                    {
                        HttpContext.Session.SetInt32("Id",user.Company_ID);
                        HttpContext.Session.SetString("Name", user.First_Name+" "+user.Last_Name);
                        HttpContext.Session.SetString("Picture", user.Profile_Pic);
                        HttpContext.Session.SetString("Is_Admin", user.Is_Admin.ToString());
                        return RedirectToAction("Index", "Tool");
                    }
                    else
                    {
                        TempData["invalidPassword"] = "Invalid Password";
                        TempData["redPass"] = "redPass";
                        return View(credentials);
                    }
                }
            }
            TempData["invalidUser"] = "Email/Company ID not found";
            TempData["redUser"] = "redUser";
            return View(credentials);
        }

        public void OnGet()
        {

        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
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