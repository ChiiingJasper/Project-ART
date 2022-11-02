using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Dynamic;
using System.Net;

namespace Project_ART.Controllers
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
            IEnumerable<TableJobApplication> objTableJobApplicationList = _db.JobApplication;
            return View(objTableJobApplicationList);
        }

        public IActionResult CreateJobApplication()
        {
            return View();
        }

        public IActionResult ViewJobApplication(int? id)
        {
            dynamic obj = new ExpandoObject();

            if (id == null || id == 0)
            {
                return NotFound();
            }
            obj.JobApplication = _db.JobApplication.Find(id);

            if (obj.JobApplication == null)
            {
                return NotFound();
            }
            try
            {
                obj.Responsibility = _db.Responsibility.Where(x => x.Job_Application_ID == id);
                obj.Qualification = _db.Qualification.Where(x => x.Job_Application_ID == id);
                obj.Benefit = _db.Benefit.Where(x => x.Job_Application_ID == id);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            

            return View(obj);
        }


        [HttpPost]
        public async Task<IActionResult> SubmitJobListing()
        {
            var data = Request.Form;
            if (Request.Form.Files.Any())
            {
                TableJobApplication jobApplication = new TableJobApplication();
                jobApplication.Job = data["jobInput"];
                jobApplication.Job_Description = data["jobDesc"];
                jobApplication.Date_Published = DateOnly.FromDateTime(DateTime.Now)+"";
                jobApplication.Date_End = data["dateEnd"];
                jobApplication.Vacancy = Int32.Parse(data["vacancy"]);
                jobApplication.Salary = data["salary"];
                jobApplication.Job_Nature = data["jobNature"];
                jobApplication.Province = data["province"];
                jobApplication.City = data["city"];

                _db.JobApplication.Add(jobApplication);
                _db.SaveChanges();

                string photoName = jobApplication.Job_Application_ID + jobApplication.Job;
                jobApplication.Icon = photoName+".png";

                _db.JobApplication.Update(jobApplication);

                TableResponsibility responsibility = new TableResponsibility();
                responsibility.Job_Application_ID = jobApplication.Job_Application_ID;
                responsibility.Responsibility = data["responsibility"];
                responsibility.Description = data["responsibilityDesc"];
                _db.Responsibility.Add(responsibility);

                TableQualification qualification = new TableQualification();
                qualification.Job_Application_ID = jobApplication.Job_Application_ID;
                qualification.Qualification = data["qualification"];
                qualification.Description = data["qualificationDesc"];
                _db.Qualification.Add(qualification);

                TableBenefit benefit = new TableBenefit();
                benefit.Job_Application_ID = jobApplication.Job_Application_ID;
                benefit.Benefit = data["benefit"];
                benefit.Description = data["benefitDesc"];
                _db.Benefit.Add(benefit);

                _db.SaveChanges();


                var photo = Request.Form.Files["icon"];
                if (photo != null)
                {
                    string UploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles");
                    string PhotoPath = Path.Combine(UploadFolder + "/Photo", photoName);
                    await photo.CopyToAsync(new FileStream(PhotoPath + ".png", FileMode.Create));
                }

            }
            return Json(HttpStatusCode.BadRequest);
        }
                

    }
}
