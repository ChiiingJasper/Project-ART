using Microsoft.AspNetCore.Mvc;
using Project_ART.Data;
using Project_ART.Models;

namespace Project_ART.Controllers
{
    public class ToolController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ToolController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var id = HttpContext.Session.GetInt32("Id");
            ViewBag.currentUser = _db.Users.SingleOrDefault(x => x.Company_ID == id);
            return View();
        }

        public IActionResult TableEmployee()
        {
            IEnumerable<TableUser> objTableEmployeeList = _db.Users;
            return View(objTableEmployeeList);
        }

        public IActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEmployee(TableUser obj)
        {
            _db.Users.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("TableEmployee");
        }

        public IActionResult UpdateEmployee(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var userFromDb = _db.Users.Find(id);

            if (userFromDb == null)
            {
                return NotFound();
            }
            return View(userFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateEmployee(TableUser obj)
        {
            if (ModelState.IsValid)
            {
                _db.Users.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("TableEmployee");
            }
            return View(obj);

        }

        [HttpGet]
        public IActionResult DeleteEmployee(int? id)
        {
            var employeeFromDb = _db.Users.Find(id);
            _db.Users.Remove(employeeFromDb);
            _db.SaveChanges();
            return RedirectToAction("TableEmployee");
        }

    }
}
