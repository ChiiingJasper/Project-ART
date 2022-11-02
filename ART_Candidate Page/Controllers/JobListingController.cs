using ART_Candidate_Page.Data;
using ART_Candidate_Page.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
using Org.BouncyCastle.Crypto.Generators;
using System.Dynamic;
using System.Net;
using System.Net.Mail;

namespace ART_Candidate_Page.Controllers
{
    public class JobListingController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JobListingController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            IEnumerable<TableJobApplication> objTableJobApplicationList = _db.JobApplication;
            return View(objTableJobApplicationList);
        }
        public IActionResult JobDesc(int? id)
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
        public async Task<IActionResult> SaveCandidateDetails()
        {
            
            var data = Request.Form;
            if (Request.Form.Files.Any())
            {


                TableResume resume = new TableResume();
                TableIntroduction introduction = new TableIntroduction();
                TableCandidate candidate = new TableCandidate { Resume = resume, Introduction = introduction };

                candidate.First_Name = data["First Name"];
                candidate.Last_Name = data["Last Name"];
                string MI = data["Middle Initial"];
                candidate.Middle_Initital = MI.ToCharArray()[0];
                candidate.Email = data["Email"];
                candidate.Mobile_Number = data["Mobile Number"];
                candidate.Website = data["Website"];
                candidate.Province = data["Province"];
                candidate.City = data["City"];
                candidate.Job_Application_ID = int.Parse(data["JobID"]);
                _db.Candidate.Add(candidate);
                _db.SaveChanges();

                TableStatus status = new TableStatus();
                status.Candidate = candidate;
                _db.Status.Add(status);
                _db.SaveChanges();

                string fileName = candidate.Candidate_ID + candidate.Last_Name +"_"+candidate.First_Name;

                candidate.Photo = fileName + ".png";
                introduction.Introduction_Video = fileName+".mp4";
                resume.Resume = fileName + ".pdf";
                _db.Candidate.Update(candidate);
                _db.Resume.Update(resume);
                _db.Introduction.Update(introduction);
                _db.SaveChanges();
                string hash = BCrypt.Net.BCrypt.HashPassword(candidate.Candidate_ID + candidate.First_Name + candidate.Last_Name);
                string link = _httpContextAccessor.HttpContext.Request.Host.Value + "/Home/EmailConfirm/?id=" + candidate.Candidate_ID + "&hash=" + hash+"&jobID="+ candidate.Job_Application_ID;
                string subject = "Email Confirmation";
                string body = "<h3>Hi " + candidate.First_Name +" "+ candidate.Last_Name + "!" +
                    " Thank you for your application, " +
                    "please click the link below to confirm your email so we can process your details.<br><br>"
                    + "<a href=https://" +link+ ">Click here to Confirm Your Email</a>"  +
                    "<br><br>Sincerly Yours, <br>Alliance Recruitment Team</h3>";
                SendMail(candidate.Email, subject, body);



                var photo = Request.Form.Files["Photo"];
                var cv = Request.Form.Files["Resume"];
                var video = Request.Form.Files["Introduction Video"];
                
                if ((photo ?? cv ?? video) != null)
                {

                    string UploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles");

                    string PhotoPath = Path.Combine(UploadFolder+"/Photos", fileName);
                    string ResumePath = Path.Combine(UploadFolder + "/Resume", fileName);
                    string VideoPath = Path.Combine(UploadFolder + "/Video", fileName);

                    await photo.CopyToAsync(new FileStream(PhotoPath + ".png", FileMode.Create));
                    await cv.CopyToAsync(new FileStream(ResumePath + ".pdf", FileMode.Create));
                    await video.CopyToAsync(new FileStream(VideoPath + ".mp4", FileMode.Create));

                }

                




            }
            return Json(HttpStatusCode.BadRequest);
        }


        public bool SendMail(string email, string subject, string body)
        {
            var sysLogin = "alliancerecruitmentteam@outlook.com";
            var sysPass = "@lliancerecruitmenttool2022";
            var sysAddress = new MailAddress(sysLogin, "Alliance Recruitment Team");

            var receiverAddress = new MailAddress(email);

            var smtp = new SmtpClient
            {
                Host = "smtp-mail.outlook.com",  
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(sysLogin, sysPass)
            };

            using (var message = new MailMessage(sysAddress, receiverAddress) { Subject = subject, Body = body })
            {
                message.IsBodyHtml = true;  
                smtp.Send(message);
            }

            return true;
        }



    }
}
