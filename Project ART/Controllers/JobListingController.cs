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
using System.Collections;
using System.Drawing.Imaging;
using Microsoft.ML;

namespace Project_ART.Controllers
{
    public class JobListingController : Controller
    {
        private readonly ApplicationDbContext _db;

        public JobListingController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(int? jobID, int? openID)
        {
            bool deleteFlag = false;
            IEnumerable<TableJobApplication> objTableJobApplicationList = _db.JobApplication.Where(x => x.Is_Deleted == deleteFlag);


            if(jobID != null)
            {
                var candidateStatus = _db.Status.Where(x => x.Is_Deleted == false && (x.Status == "Approved" || x.Status == "Pending")).ToList();
                var Candidates = from status in candidateStatus
                                 join candidate in _db.Candidate.Where(x => x.Is_Deleted == false && x.Job_Application_ID == jobID) on status.Candidate_ID equals candidate.Candidate_ID
                                 select new
                                 {
                                  Candidate_ID = candidate.Candidate_ID
                                 };

                foreach(var i in Candidates)
                {
                    var NotHired = _db.Status.Find(i.Candidate_ID);
                    NotHired.Status = "Not Hired";
                    _db.Status.Update(NotHired);
                    
                }
                var jobStatus = _db.JobApplication.Find(jobID);
                jobStatus.Is_Open = false;
                _db.JobApplication.Update(jobStatus);

                _db.SaveChanges();
            }

            if (openID != null)
            {
                var jobStatus = _db.JobApplication.Find(openID);
                jobStatus.Is_Open = true;
                _db.JobApplication.Update(jobStatus);

                _db.SaveChanges();
            }






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

            var pendingCandidates =
                                         from status in _db.Status.Where(x => x.Is_Deleted == false && x.Status == "Pending")
                                         join candidate in _db.Candidate.Where(x => x.Is_Deleted == false && x.Job_Application_ID == id) on status.Candidate_ID equals candidate.Candidate_ID
                                         select new
                                         {
                                             Candidate_ID = candidate.Candidate_ID,
                                         };

            ViewBag.PendingCount = pendingCandidates.Count();



            var approvedCandidates =
                             from status in _db.Status.Where(x => x.Is_Deleted == false && x.Status == "Approved")
                             join candidate in _db.Candidate.Where(x => x.Is_Deleted == false && x.Job_Application_ID == id) on status.Candidate_ID equals candidate.Candidate_ID
                             select new
                             {
                                 Candidate_ID = candidate.Candidate_ID,
                             };

            ViewBag.ApprovedCount = approvedCandidates.Count();

            var hiredCandidates =
                             from status in _db.Status.Where(x => x.Is_Deleted == false && x.Status == "Hired")
                             join candidate in _db.Candidate.Where(x => x.Is_Deleted == false && x.Job_Application_ID == id) on status.Candidate_ID equals candidate.Candidate_ID
                             select new
                             {
                                 Candidate_ID = candidate.Candidate_ID,
                             };

            ViewBag.HiredCount = hiredCandidates.Count();

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

                string UploadFolder = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles");
                string PhotoPath = System.IO.Path.Combine(UploadFolder + "/Photo", photoName);

                jobApplication.Icon = photoName + ".png";

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


            var pendingCandidates =
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

            ViewBag.Candidates = pendingCandidates.ToList();

            int pendingCount = pendingCandidates.Count();
            ViewBag.PendingCount = pendingCount;
            
            foreach (var i in pendingCandidates.OrderByDescending(x => x.Resume_Score).ToList())
            {
          
                var sampleData = new MLCandidateRanking.ModelInput()
                {
                    DISC_Personality = i.DISC,
                    Resume_Score = (float)i.Resume_Score,
                };

                
                //Load model and predict output
                var result = MLCandidateRanking.Predict(sampleData).Score;
                int finalResult = (int)(result[1] * 10 + (i.Resume_Score*.9));

                TempData[i.Candidate_ID.ToString()] = finalResult.ToString() + "%";
                TempData[i.Candidate_ID.ToString() + "rank"] = ((100 - finalResult) * .1).ToString();
            }

            


            var approvedCandidates =
                             from status in _db.Status.Where(x => x.Is_Deleted == false && x.Status == "Approved")
                             join candidate in _db.Candidate.Where(x => x.Is_Deleted == false && x.Job_Application_ID == id) on status.Candidate_ID equals candidate.Candidate_ID
                             select new
                             {
                                 Candidate_ID = candidate.Candidate_ID,
                             };

            ViewBag.ApprovedCount = approvedCandidates.Count();

            var hiredCandidates =
                             from status in _db.Status.Where(x => x.Is_Deleted == false && x.Status == "Hired")
                             join candidate in _db.Candidate.Where(x => x.Is_Deleted == false && x.Job_Application_ID == id) on status.Candidate_ID equals candidate.Candidate_ID
                             select new
                             {
                                 Candidate_ID = candidate.Candidate_ID,
                             };

            ViewBag.HiredCount = hiredCandidates.Count();

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
                approvedCandidate.Hired_By = HttpContext.Session.GetInt32("Id");
                _db.Status.Update(approvedCandidate);
                _db.SaveChanges();
            }


            var Candidates =
                             from status in _db.Status.Where(x => x.Is_Deleted == false && x.Status == "Approved")
                             join candidate in _db.Candidate.Where(x => x.Is_Deleted == false && x.Job_Application_ID == id) on status.Candidate_ID equals candidate.Candidate_ID
                             join resume in _db.Resume.Where(x => x.Is_Deleted == false) on candidate.Resume_ID equals resume.Resume_ID
                             join video in _db.Introduction.Where(x => x.Is_Deleted == false) on candidate.Introduction_ID equals video.Introduction_ID
                             join assessment in _db.Assessment on candidate.Assessment_ID equals assessment.Assessment_ID
                             join exam in _db.Exam on assessment.Assessment_ID equals exam.Exam_ID
                             join interview in _db.Interview on assessment.Assessment_ID equals interview.Interview_ID
                             select new
                             {
                                 Candidate_ID = candidate.Candidate_ID,
                                 First_Name = candidate.First_Name,
                                 Last_Name = candidate.Last_Name,
                                 Middle_Initial = candidate.Middle_Initital,
                                 DISC = video.DISC_Trait,
                                 Resume_Score = resume.Resume_Score,
                                 Photo = candidate.Photo,
                                 Exam = exam.Exam_Score,
                                 Interview = interview.Interview_Score
                             };

            var weighting = _db.Weighting.Find(1);
            double personalityScore = (double)(weighting.Personality);
            double resumeScore = (double)(weighting.Resume) * .01;
            double examScore = (double)(weighting.Exam) * .01;
            double interviewScore = (double)(weighting.Interview) * .01;

            foreach (var i in Candidates)
            {
                
                var sampleData = new MLCandidateRanking.ModelInput()
                {
                    DISC_Personality = i.DISC,
                    Resume_Score = (float)i.Resume_Score,
                };

                

                //Load model and predict output
                var result = MLCandidateRanking.Predict(sampleData).Score;

                if (i.Exam == 0 || i.Interview == 0)
                {
                    var finalResult = "NA";
                    TempData[i.Candidate_ID.ToString()] = finalResult.ToString();
                    TempData[i.Candidate_ID.ToString() + "rank"] = (100 - i.Resume_Score).ToString() + finalResult.ToString();
                }
                else
                {
                    var finalResult = (int)(result[1] * personalityScore + (i.Resume_Score * resumeScore) + (i.Exam * examScore) + (i.Interview * interviewScore));
                    TempData[i.Candidate_ID.ToString()] = finalResult.ToString() + "%";
                    TempData[i.Candidate_ID.ToString() + "rank"] = ((100 - finalResult) * .1).ToString();
                }




              
            }

            ViewBag.Candidates = Candidates.ToList();
            ViewBag.ApprovedCount = Candidates.Count();


            var pendingCandidates =
                                         from status in _db.Status.Where(x => x.Is_Deleted == false && x.Status == "Pending")
                                         join candidate in _db.Candidate.Where(x => x.Is_Deleted == false && x.Job_Application_ID == id) on status.Candidate_ID equals candidate.Candidate_ID
                                         select new
                                         {
                                             Candidate_ID = candidate.Candidate_ID,
                                         };

            ViewBag.PendingCount = pendingCandidates.Count();


            

            var hiredCandidates =
                             from status in _db.Status.Where(x => x.Is_Deleted == false && x.Status == "Hired")
                             join candidate in _db.Candidate.Where(x => x.Is_Deleted == false && x.Job_Application_ID == id) on status.Candidate_ID equals candidate.Candidate_ID
                             select new
                             {
                                 Candidate_ID = candidate.Candidate_ID,
                             };

            ViewBag.HiredCount = hiredCandidates.Count();

            return View(obj);


        }
        public IActionResult ApprovedCandidate(int? id)
        {
            var candidate = _db.Candidate.Find(id);
            ViewBag.Candidates = candidate;

            ViewBag.MatchedSkills = _db.Data.Where(x => x.Resume_ID == candidate.Resume_ID && x.Is_Deleted == false && x.Skill_Matched == true);
            ViewBag.OtherSkills = _db.Data.Where(x => x.Resume_ID == candidate.Resume_ID && x.Is_Deleted == false && x.Skill_Matched == false);
            var assessmentID = _db.Assessment.Find(candidate.Assessment_ID);
            var exam = _db.Exam.Find(assessmentID.Exam_ID);
            var interview = _db.Interview.Find(assessmentID.Assessment_ID);

            ViewBag.Exam = exam.Exam_Score;
            ViewBag.Interview = interview.Interview_Score;

            return View();
        }
        public IActionResult Hired(int? id ,int? removeCandidate)
        {
            if (removeCandidate != null)
            {
                var approvedCandidate = _db.Status.Find(removeCandidate);
                approvedCandidate.Status = "Pending";
                _db.Status.Update(approvedCandidate);
                _db.SaveChanges();
            }

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
                             join assessment in _db.Assessment on candidate.Assessment_ID equals assessment.Assessment_ID
                             join exam in _db.Exam on assessment.Assessment_ID equals exam.Exam_ID
                             join interview in _db.Interview on assessment.Assessment_ID equals interview.Interview_ID
                             select new
                             {
                                 Candidate_ID = candidate.Candidate_ID,
                                 First_Name = candidate.First_Name,
                                 Last_Name = candidate.Last_Name,
                                 Middle_Initial = candidate.Middle_Initital,
                                 DISC = video.DISC_Trait,
                                 Resume_Score = resume.Resume_Score,
                                 Photo = candidate.Photo,
                                 Exam = exam.Exam_Score,
                                 Interview = interview.Interview_Score
                             };



            int index = 0;
            foreach (var i in Candidates.OrderByDescending(x => x.Resume_Score).ToList())
            {

                var sampleData = new MLCandidateRanking.ModelInput()
                {
                    DISC_Personality = i.DISC,
                    Resume_Score = (float)i.Resume_Score,
                };


                //Load model and predict output
                var result = MLCandidateRanking.Predict(sampleData).Score;

                if (i.Exam == 0 || i.Interview == 0)
                {
                    var finalResult = "NA";
                    TempData[i.Candidate_ID.ToString()] = finalResult.ToString();
                    TempData[i.Candidate_ID.ToString() + "rank"] = (100 - i.Resume_Score).ToString()+finalResult.ToString();
                }
                else
                {
                    var finalResult = (int)(result[1] * 10 + (i.Resume_Score * .20) + (i.Exam * .30) + (i.Interview * .40) + (Candidates.Count() - index));
                    TempData[i.Candidate_ID.ToString()] = finalResult.ToString() + "%";
                    TempData[i.Candidate_ID.ToString() + "rank"] = ((100 - finalResult) * .1).ToString();
                }





                index++;
            }



            ViewBag.Candidates = Candidates.OrderByDescending(x => x.Resume_Score).ToList();

            var approvedCandidates =
                                         from status in _db.Status.Where(x => x.Is_Deleted == false && x.Status == "Approved")
                                         join candidate in _db.Candidate.Where(x => x.Is_Deleted == false && x.Job_Application_ID == id) on status.Candidate_ID equals candidate.Candidate_ID
                                         select new
                                         {
                                             Candidate_ID = candidate.Candidate_ID,
                                         };

            ViewBag.ApprovedCount = approvedCandidates.Count();





            var pendingCandidates =
                                         from status in _db.Status.Where(x => x.Is_Deleted == false && x.Status == "Pending")
                                         join candidate in _db.Candidate.Where(x => x.Is_Deleted == false && x.Job_Application_ID == id) on status.Candidate_ID equals candidate.Candidate_ID
                                         select new
                                         {
                                             Candidate_ID = candidate.Candidate_ID,
                                         };

            ViewBag.PendingCount = pendingCandidates.Count();





            ViewBag.HiredCount = Candidates.Count();

            return View(obj);
        }
        public IActionResult HiredCandidate(int? id)
        {
            var candidate = _db.Candidate.Find(id);
            ViewBag.Candidates = candidate;

            ViewBag.MatchedSkills = _db.Data.Where(x => x.Resume_ID == candidate.Resume_ID && x.Is_Deleted == false && x.Skill_Matched == true);
            ViewBag.OtherSkills = _db.Data.Where(x => x.Resume_ID == candidate.Resume_ID && x.Is_Deleted == false && x.Skill_Matched == false);
            var assessmentID = _db.Assessment.Find(candidate.Assessment_ID);
            var exam = _db.Exam.Find(assessmentID.Exam_ID);
            var interview = _db.Interview.Find(assessmentID.Assessment_ID);

            ViewBag.Exam = exam.Exam_Score;
            ViewBag.Interview = interview.Interview_Score;

            return View();
        }








        [HttpPost]
        public async Task<IActionResult> SubmitAssessment()
        {

            var data = Request.Form;
            
                int candidateID = Int32.Parse(data["candidateID"]);
                var score = Int32.Parse(data["score"]);
                
                var status = _db.Status.Find(candidateID);
                status.Assessed_By = HttpContext.Session.GetInt32("Id");
                var candidate = _db.Candidate.Find(candidateID);
                var assessment = _db.Assessment.Find(candidate.Assessment_ID);
                var exam = _db.Exam.Find(assessment.Exam_ID);

                exam.Exam_Score = score;
                _db.Status.Update(status);
                _db.Exam.Update(exam);
                _db.SaveChanges();
            
                
            return Json(HttpStatusCode.BadRequest);
        }


        [HttpPost]
        public async Task<IActionResult> SubmitInterview()
        {

            var data = Request.Form;

            int candidateID = Int32.Parse(data["candidateID"]);
            var score = Int32.Parse(data["score"]);

            var candidate = _db.Candidate.Find(candidateID);
            var assessment = _db.Assessment.Find(candidate.Assessment_ID);
            var interview = _db.Interview.Find(assessment.Exam_ID);

            interview.Interview_Score = score;
            _db.Interview.Update(interview);
            _db.SaveChanges();


            return Json(HttpStatusCode.BadRequest);
        }


    }
}
