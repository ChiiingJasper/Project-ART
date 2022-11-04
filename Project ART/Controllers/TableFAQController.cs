using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_ART.Controllers
{
    public class TableFAQController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TableFAQController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<TableFAQ> objTableFAQList = _db.FAQ;
            return View(objTableFAQList);
        }
        public IActionResult CreateFAQ()
        {
            ViewBag.Answered = GetAnswered();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFAQ(TableFAQ obj)
        {
            _db.FAQ.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateFAQ(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var faqFromDb = _db.FAQ.Find(id);

            if (faqFromDb == null)
            {
                return NotFound();
            }

            ViewBag.Answered = GetAnswered();
            return View(faqFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateFAQ(TableFAQ obj)
        {
            _db.FAQ.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult DeleteFAQ(int? id)
        {

            bool faqFlag = true;
            var faqFromDb = _db.FAQ.Find(id);
            var faq = new TableFAQ() { Question_ID = faqFromDb.Question_ID, Is_Deleted = faqFlag };

            _db.FAQ.Attach(faq);
            _db.Entry(faq).Property(x => x.Is_Deleted).IsModified = true;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        private List<SelectListItem> GetAnswered()
        {
            var lstUser = new List<SelectListItem>();
            foreach (var item in _db.User)
            {
                lstUser.Add(new SelectListItem()
                {
                    Value = item.Company_ID.ToString(),
                    Text = item.First_Name
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select User who Answered----"

            };

            lstUser.Insert(0, defItem);

            return lstUser;
        }
    }
}
