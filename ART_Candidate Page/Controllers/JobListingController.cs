using ART_Candidate_Page.Data;
using ART_Candidate_Page.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ART_Candidate_Page.Controllers
{
    public class JobListingController : Controller
    {
        private readonly ApplicationDbContext _db;

        public JobListingController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult JobDesc()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveCandidateDetails()
        {
            if (Request.Form.Files.Any())
            {
                var data = Request;
                TableCandidate obj = new TableCandidate();
                obj.First_Name = data.Form["First Name"];
                obj.Last_Name = data.Form["Last Name"];
                obj.Middle_Initital = data.Form["Middle Initial"];
                obj.Email = data.Form["Email"];
                obj.MobileNumber = data.Form["Mobile Number"];
                obj.Website = data.Form["Website"];
                obj.Province = data.Form["Province"];
                obj.City = data.Form["City"];
                _db.Candidates.Add(obj);
                _db.SaveChanges();

                var file = Request.Form.Files["video-blob"];
                if (file != null)
                {
                    string UploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles/Video");
                    string UploadPath = Path.Combine(UploadFolder, "Jasper_Ching.mp4");
                    await file.CopyToAsync(new FileStream(UploadPath, FileMode.Create));
                }
                

            }
            return Json(HttpStatusCode.OK);
        }



    }
}
