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
            return View();
        }

        public IActionResult TableEmployee()
        {
            IEnumerable<TableEmployee> objTableEmployeeList = _db.Employees;
            return View(objTableEmployeeList);
        }

        public IActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateEmployee(TableEmployee obj)
        {
            _db.Employees.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("TableEmployee");
        }

        public IActionResult UpdateEmployee(int? id)
        {
            var employeeFromDb = _db.Employees.Find(id);

            return View(employeeFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateEmployee(TableEmployee obj)
        {
            _db.Employees.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("TableEmployee");
        }

        [HttpGet]
        public IActionResult DeleteEmployee(int? id)
        {
            var employeeFromDb = _db.Employees.Find(id);
            _db.Employees.Remove(employeeFromDb);
            _db.SaveChanges();
            return RedirectToAction("TableEmployee");
        }

    }
}
