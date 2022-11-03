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
            IEnumerable<TableKeyword> objTableKeywordList = _db.KeyWord;
            return View(objTableKeywordList);
        }

        /*
        public IActionResult TableKeyword()
        {
            IEnumerable<TableKeyword> objTableKeywordList = _db.KeyWords;
            return View(objTableKeywordList);
        }
        */

        public IActionResult CreateKeyword()
        {
            ViewBag.Introductions = GetIntroductions();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateKeyword(TableKeyword obj)
        {
            _db.KeyWord.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateKeyword(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var keywordFromDb = _db.KeyWord.Find(id);

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
            _db.KeyWord.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult DeleteIntroduction(int? id)
        {

            var keywordFromDb = _db.KeyWord.Find(id);
            if(keywordFromDb != null)
            {
                _db.KeyWord.Remove(keywordFromDb);
                _db.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }

        private List<SelectListItem> GetIntroductions()
        {
            var lstIntroductions = new List<SelectListItem>();
            foreach (var item in _db.Introduction)
            {
                lstIntroductions.Add(new SelectListItem()
                {
                    Value = item.Introduction_ID.ToString(),
                    
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
