using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Dynamic;
using System.Net;
using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

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
            bool deleteFlag = false;
            IEnumerable<TableJobApplication> objTableJobApplicationList = _db.JobApplication.Where(x => x.Is_Deleted == deleteFlag);
            
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
            var PendingCount = _db.Status
            .Where(o => o.Status == "Pending" && o.Is_Deleted == false)
            .Count();

            ViewBag.PendingCount = PendingCount;

            var ApprovedCount = _db.Status
            .Where(o => o.Status == "Approved" && o.Is_Deleted == false)
            .Count();

            ViewBag.ApprovedCount = ApprovedCount;

            var HiredCount = _db.Status
            .Where(o => o.Status == "Hired" && o.Is_Deleted == false)
            .Count();

            ViewBag.HiredCount = HiredCount;

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
                jobApplication.Date_Published = DateOnly.FromDateTime(DateTime.Now).ToString("MMM dd yyyy");
                jobApplication.Date_End = DateOnly.FromDateTime(DateTime.Parse(data["dateEnd"])).ToString("MMM dd yyyy");
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

                

                for (int i = 0; i < int.Parse(data["responsibilityCount"]); i++)
                {
                    TableResponsibility responsibility = new TableResponsibility();
                    responsibility.Job_Application_ID = jobApplication.Job_Application_ID;
                    responsibility.Responsibility = data["responsibility"+i];
                    responsibility.Description = data["responsibilityDesc"+i];
                    _db.Responsibility.Add(responsibility);
                }
                

                
                for (int i = 0; i < int.Parse(data["qualificationCount"]); i++)
                {
                    TableQualification qualification = new TableQualification();
                    qualification.Job_Application_ID = jobApplication.Job_Application_ID;
                    qualification.Qualification = data["qualification" + i];
                    qualification.Description = data["qualificationDesc" + i];
                    _db.Qualification.Add(qualification);
                }
                

                
                for (int i = 0; i < int.Parse(data["benefitCount"]); i++)
                {
                    TableBenefit benefit = new TableBenefit();
                    benefit.Job_Application_ID = jobApplication.Job_Application_ID;
                    benefit.Benefit = data["benefit" + i];
                    benefit.Description = data["benefitDesc" + i];
                    _db.Benefit.Add(benefit);
                }


                _db.SaveChanges();


                var photo = Request.Form.Files["icon"];
                if (photo != null)
                {
                    string UploadFolder = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles");
                    string PhotoPath = System.IO.Path.Combine(UploadFolder + "/Photo", photoName);
                    await photo.CopyToAsync(new FileStream(PhotoPath + ".png", FileMode.Create));
                }

            }
            return Json(HttpStatusCode.BadRequest);
        }

        public IActionResult Pending(int? id, int? approveCandidate)
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

            if(approveCandidate != null)
            {
               var approvedCandidate = _db.Status.Find(approveCandidate);
                approvedCandidate.Status = "Approved";
                approvedCandidate.Approved_By = HttpContext.Session.GetInt32("Id");
                _db.Status.Update(approvedCandidate);
                _db.SaveChanges();
            }

 
            var Candidates =
                             from status in _db.Status.Where(x => x.Is_Deleted == false && x.Status == "Pending")
                             join candidate in _db.Candidate.Where(x => x.Is_Deleted == false && x.Job_Application_ID == id) on status.Candidate_ID equals candidate.Candidate_ID
                             join resume in _db.Resume.Where(x => x.Is_Deleted == false) on candidate.Resume_ID equals resume.Resume_ID
                             join video in _db.Introduction.Where(x => x.Is_Deleted == false) on candidate.Introduction_ID equals video.Introduction_ID
                             select new
                             {
                                 Candidate_ID = candidate.Candidate_ID,
                                 First_Name = candidate.First_Name,
                                 Last_Name = candidate.Last_Name,
                                 Middle_Initial = candidate.Middle_Initital,
                                 DISC = video.DISC_Trait,
                                 Resume_Score = resume.Resume_Score,
                                 Photo = candidate.Photo
                             };
            var candidateList = Candidates.OrderByDescending(x => x.Resume_Score).ToList();
            
            ViewBag.Candidates = candidateList;

            TempData["Dosdos"] = 62;
            TempData["Castanares"] = 35;
            TempData["Miscala"] = 27;

            var PendingCount = _db.Status
            .Where(o => o.Status == "Pending" && o.Is_Deleted == false)
            .Count();

            ViewBag.PendingCount = PendingCount;

            var ApprovedCount = _db.Status
            .Where(o => o.Status == "Approved" && o.Is_Deleted == false)
            .Count();

            ViewBag.ApprovedCount = ApprovedCount;

            var HiredCount = _db.Status
            .Where(o => o.Status == "Hired" && o.Is_Deleted == false)
            .Count();

            ViewBag.HiredCount = HiredCount;

            return View(obj);

            
        }

        public IActionResult PendingCandidate(int? id)
        {
            var candidate = _db.Candidate.Find(id);
            ViewBag.Candidates = candidate;

            ViewBag.MatchedSkills = _db.Data.Where(x => x.Resume_ID == candidate.Resume_ID && x.Is_Deleted == false && x.Skill_Matched == true);
            ViewBag.OtherSkills = _db.Data.Where(x => x.Resume_ID == candidate.Resume_ID && x.Is_Deleted == false && x.Skill_Matched == false);
            return View();
        }


        public IActionResult Approved(int? id, int? hireCandidate)
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

            if (hireCandidate != null)
            {
                var approvedCandidate = _db.Status.Find(hireCandidate);
                approvedCandidate.Status = "Hired";
                approvedCandidate.Approved_By = HttpContext.Session.GetInt32("Id");
                _db.Status.Update(approvedCandidate);
                _db.SaveChanges();
            }


            var Candidates =
                             from status in _db.Status.Where(x => x.Is_Deleted == false && x.Status == "Approved")
                             join candidate in _db.Candidate.Where(x => x.Is_Deleted == false && x.Job_Application_ID == id) on status.Candidate_ID equals candidate.Candidate_ID
                             join resume in _db.Resume.Where(x => x.Is_Deleted == false) on candidate.Resume_ID equals resume.Resume_ID
                             join video in _db.Introduction.Where(x => x.Is_Deleted == false) on candidate.Introduction_ID equals video.Introduction_ID
                             select new
                             {
                                 Candidate_ID = candidate.Candidate_ID,
                                 First_Name = candidate.First_Name,
                                 Last_Name = candidate.Last_Name,
                                 Middle_Initial = candidate.Middle_Initital,
                                 DISC = video.DISC_Trait,
                                 Resume_Score = resume.Resume_Score,
                                 Photo = candidate.Photo
                             };

            ViewBag.Candidates = Candidates.ToList();


            var PendingCount = _db.Status
            .Where(o => o.Status == "Pending" && o.Is_Deleted == false)
            .Count();

            ViewBag.PendingCount = PendingCount;

            var ApprovedCount = _db.Status
            .Where(o => o.Status == "Approved" && o.Is_Deleted == false)
            .Count();

            ViewBag.ApprovedCount = ApprovedCount;

            var HiredCount = _db.Status
            .Where(o => o.Status == "Hired" && o.Is_Deleted == false)
            .Count();

            ViewBag.HiredCount = HiredCount;

            return View(obj);


        }
        public IActionResult ApprovedCandidate(int? id)
        {
            var candidate = _db.Candidate.Find(id);
            ViewBag.Candidates = candidate;

            ViewBag.MatchedSkills = _db.Data.Where(x => x.Resume_ID == candidate.Resume_ID && x.Is_Deleted == false && x.Skill_Matched == true);
            ViewBag.OtherSkills = _db.Data.Where(x => x.Resume_ID == candidate.Resume_ID && x.Is_Deleted == false && x.Skill_Matched == false);
            return View();
        }
        public IActionResult Hired(int? id)
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

          


            var Candidates =
                             from status in _db.Status.Where(x => x.Is_Deleted == false && x.Status == "Hired")
                             join candidate in _db.Candidate.Where(x => x.Is_Deleted == false && x.Job_Application_ID == id) on status.Candidate_ID equals candidate.Candidate_ID
                             join resume in _db.Resume.Where(x => x.Is_Deleted == false) on candidate.Resume_ID equals resume.Resume_ID
                             join video in _db.Introduction.Where(x => x.Is_Deleted == false) on candidate.Introduction_ID equals video.Introduction_ID
                             select new
                             {
                                 Candidate_ID = candidate.Candidate_ID,
                                 First_Name = candidate.First_Name,
                                 Last_Name = candidate.Last_Name,
                                 Middle_Initial = candidate.Middle_Initital,
                                 DISC = video.DISC_Trait,
                                 Resume_Score = resume.Resume_Score,
                                 Photo = candidate.Photo
                             };

            ViewBag.Candidates = Candidates.ToList();


            var PendingCount = _db.Status
            .Where(o => o.Status == "Pending" && o.Is_Deleted == false)
            .Count();

            ViewBag.PendingCount = PendingCount;

            var ApprovedCount = _db.Status
            .Where(o => o.Status == "Approved" && o.Is_Deleted == false)
            .Count();

            ViewBag.ApprovedCount = ApprovedCount;

            var HiredCount = _db.Status
            .Where(o => o.Status == "Hired" && o.Is_Deleted == false)
            .Count();

            ViewBag.HiredCount = HiredCount;

            return View(obj);
        }

       
    }
}
