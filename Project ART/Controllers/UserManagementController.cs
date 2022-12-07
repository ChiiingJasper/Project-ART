using Microsoft.AspNetCore.Mvc;
using Project_ART.Data;
using Project_ART.Models;
using System.Dynamic;

namespace Project_ART.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly ApplicationDbContext _db;
        public UserManagementController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            bool deleteFlag = false;
            IEnumerable<TableUser> obj = _db.User.Where(x => x.Is_Deleted == deleteFlag);
            return View(obj);
        }

        public IActionResult EditUser(int id)
        {
            ViewBag.currentUser = _db.User.SingleOrDefault(x => x.Company_ID == id);
            var userFromDb = _db.User.Find(id);
            return View(userFromDb);
        }

        public IActionResult CreateUser(int id)
        {
           
            return View();
        }
    }
}
