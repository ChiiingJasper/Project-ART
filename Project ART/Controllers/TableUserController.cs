using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Mvc;

namespace Project_ART.Controllers
{
    public class TableUserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TableUserController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<TableUser> objTableUserList = _db.User;
            return View(objTableUserList);
        }

        /*
        public IActionResult TableUser()
        {
            IEnumerable<TableUser> objTableUserList = _db.Users;
            return View(objTableUserList);
        }
        */

        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser(TableUser obj)
        {
            obj.Password = BCrypt.Net.BCrypt.HashPassword(obj.Password);
            _db.User.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateUser(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var userFromDb = _db.User.Find(id);

            if (userFromDb == null)
            {
                return NotFound();
            }
            return View(userFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateUser(TableUser obj)
        {
            if (ModelState.IsValid)
            {
                obj.Password = BCrypt.Net.BCrypt.HashPassword(obj.Password);
                _db.User.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("TableUser");
            }
            return View(obj);

        }

        [HttpGet]
        public IActionResult DeleteUser(int? id)
        {
            var userFromDb = _db.User.Find(id);
            _db.User.Remove(userFromDb);
            _db.SaveChanges();
            return RedirectToAction("TableUser");
        }
    }
}
