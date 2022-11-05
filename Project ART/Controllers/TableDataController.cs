using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_ART.Controllers
{
    public class TableDataController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TableDataController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<TableData> objTableDataList = _db.Data;
            return View(objTableDataList);
        }

        public IActionResult CreateData()
        {
            ViewBag.Resumes = GetResume();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateData(TableData obj)
        {
            _db.Data.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateData(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var dataFromDb = _db.Data.Find(id);

            if (dataFromDb == null)
            {
                return NotFound();
            }

            ViewBag.Resumes = GetResume();
            return View(dataFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateData(TableData obj)
        {
            _db.Data.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult DeleteData(int? id)
        {

            bool dataFlag = true;
            var dataFromDb = _db.Data.Find(id);
            var data = new TableData() { Data_ID = dataFromDb.Data_ID, Is_Deleted = dataFlag };

            _db.Data.Attach(data);
            _db.Entry(data).Property(x => x.Is_Deleted).IsModified = true;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        private List<SelectListItem> GetResume()
        {
            var lstResumes = new List<SelectListItem>();
            foreach (var item in _db.Resume)
            {
                lstResumes.Add(new SelectListItem()
                {
                    Value = item.Resume_ID.ToString(),
                    Text = item.Resume

                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Resume----"

            };

            lstResumes.Insert(0, defItem);

            return lstResumes;
        }
    }
}
