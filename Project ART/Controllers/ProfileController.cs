using Microsoft.AspNetCore.Mvc;
using Project_ART.Data;
using Project_ART.Models;
using System.Net;

namespace Project_ART.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProfileController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var id = HttpContext.Session.GetInt32("Id");
            ViewBag.currentUser = _db.User.SingleOrDefault(x => x.Company_ID == id);
            var userFromDb = _db.User.Find(id);
            return View(userFromDb);
        }

        public IActionResult EditUser()
        {
            var id = HttpContext.Session.GetInt32("Id");
            ViewBag.currentUser = _db.User.SingleOrDefault(x => x.Company_ID == id);

            var userFromDb = _db.User.Find(id);
            return View(userFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(TableUser obj)
        {
            var id = HttpContext.Session.GetInt32("Id");
            var userFromDb = _db.User.Find(id);

            obj.Company_ID = (int)id;
            

            //if (obj.Password != obj.Confirm_Password)
            {
                TempData["passwordMismatch"] = "Your Password must match";
                TempData["error"] = "redborder";
                return RedirectToAction("EditUser"); ;
            }
            if (obj.Password == null)
            {
                obj.Password = userFromDb.Password;
                //obj.Confirm_Password = userFromDb.Password;
            }
            else
            {
                obj.Password = BCrypt.Net.BCrypt.HashPassword(obj.Password);
                //obj.Confirm_Password = obj.Password;
            }
            if (ModelState.IsValid)
            {
                _db.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
    }
}
