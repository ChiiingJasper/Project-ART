using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Mvc;

namespace Project_ART.Controllers
{
    public class TableDatasheetController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TableDatasheetController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TableDatasheet()
        {
            IEnumerable<TableDatasheet> objTableDatasheetList = _db.Datasheets;
            return View(objTableDatasheetList);
        }

        public IActionResult CreateDatasheet()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateDatasheet(TableDatasheet obj)
        {
            _db.Datasheets.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("TableDatasheet");
        }

        public IActionResult UpdateDatasheet(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var datasheetFromDb = _db.Datasheets.Find(id);

            if (datasheetFromDb == null)
            {
                return NotFound();
            }
            return View(datasheetFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateDatasheet(TableDatasheet obj)
        {
            if (ModelState.IsValid)
            {
                _db.Datasheets.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("TableDatasheet");
            }
            return View(obj);

        }

        [HttpGet]
        public IActionResult DeleteDatasheet(int? id)
        {
            var datasheetFromDb = _db.Datasheets.Find(id);
            _db.Datasheets.Remove(datasheetFromDb);
            _db.SaveChanges();
            return RedirectToAction("TableDatasheet");
        }
    }
}
