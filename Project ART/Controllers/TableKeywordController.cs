using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_ART.Controllers
{
    public class TableKeywordController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TableKeywordController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TableKeyword()
        {
            IEnumerable<TableKeyword> objTableKeywordList = _db.KeyWords;
            return View(objTableKeywordList);
        }

        public IActionResult CreateKeyword()
        {
            ViewBag.Introductions = GetIntroductions();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateKeyword(TableKeyword obj)
        {
            _db.KeyWords.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("TableKeyword");
        }

        public IActionResult UpdateKeyword(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var keywordFromDb = _db.KeyWords.Find(id);

            if (keywordFromDb == null)
            {
                return NotFound();
            }

            ViewBag.Introductions = GetIntroductions();
            return View(keywordFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateKeyword(TableKeyword obj)
        {
            _db.KeyWords.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("TableKeyword");

        }

        [HttpGet]
        public IActionResult DeleteIntroduction(int? id)
        {

            var keywordFromDb = _db.KeyWords.Find(id);
            if(keywordFromDb != null)
            {
                _db.KeyWords.Remove(keywordFromDb);
                _db.SaveChanges();
            }
            
            return RedirectToAction("TableKeyword");
        }

        private List<SelectListItem> GetIntroductions()
        {
            var lstIntroductions = new List<SelectListItem>();
            foreach (var item in _db.Introductions)
            {
                lstIntroductions.Add(new SelectListItem()
                {
                    Value = item.Introduction_ID.ToString(),
                    Text = item.B5_Trait
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Introduction----"

            };

            lstIntroductions.Insert(0, defItem);

            return lstIntroductions;
        }
    }
}
