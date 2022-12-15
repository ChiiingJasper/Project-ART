using ART_Candidate_Page.Data;
using ART_Candidate_Page.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Matching;
using Org.BouncyCastle.Crypto.Generators;
using System.Dynamic;
using System.Net;
using System.Net.Mail;
using System.Speech.Recognition;
using System.Speech.AudioFormat;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Globalization;
using System.Linq;

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
            IEnumerable<TableJobApplication> objTableJobApplicationList = _db.JobApplication.Where(x => x.Is_Deleted == false && x.Is_Open == true);
            var count = _db.JobApplication
            .Where(o => o.Is_Open == true && o.Is_Deleted == false)
            .Count();

            ViewBag.Count = count;
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
                TableExam exam = new TableExam { Exam_Score = 0};
                TableInterview interview = new TableInterview { Interview_Score = 0 };

                TableAssessment assessment = new TableAssessment { Exam = exam, Interview = interview };

                TableCandidate candidate = new TableCandidate { Resume = resume, Introduction = introduction , Assessment = assessment};
                candidate.First_Name = data["First Name"];
                candidate.Last_Name = data["Last Name"];
                string MI = data["Middle Initial"];
                candidate.Middle_Initital = MI.ToUpper().ToCharArray()[0];
                candidate.Email = data["Email"];
                candidate.Mobile_Number = data["Mobile Number"];
                candidate.Website = data["Website"];
                candidate.Province = data["Province"];
                candidate.City = data["City"];
                int jobID = int.Parse(data["JobID"]);
                candidate.Job_Application_ID = jobID;
                candidate.Email_Confirmed = false;
                _db.Candidate.Add(candidate);
                _db.SaveChanges();

                TableStatus status = new TableStatus();
                status.Candidate = candidate;
                _db.Status.Add(status);
                _db.SaveChanges();

                string fileName = candidate.Candidate_ID + candidate.Last_Name +"_"+candidate.First_Name;

                candidate.Photo = fileName + ".png";
                introduction.Introduction_Video = fileName+".webm";
                resume.Resume = fileName + ".pdf";
                _db.Candidate.Update(candidate);
                
                
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

                if (photo !=null && cv != null && video != null)
                {

                    string UploadFolder = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles");

                   string PhotoPath = System.IO.Path.Combine(UploadFolder+"/Photos", fileName);
                    string ResumePath = System.IO.Path.Combine(UploadFolder + "/Resume", fileName);
                   string VideoPath = System.IO.Path.Combine(UploadFolder + "/Video", fileName);
                    string AudioPath = System.IO.Path.Combine(UploadFolder + "/Speech", fileName);

                    FileStream fsPhoto = new FileStream(PhotoPath + ".png", FileMode.Create);
                    await photo.CopyToAsync(fsPhoto);
                    fsPhoto.Close();

                    FileStream fsResume = new FileStream(ResumePath + ".pdf", FileMode.Create);
                    await cv.CopyToAsync(fsResume);
                    fsResume.Close();

                    FileStream fsVideo = new FileStream(VideoPath + ".webm", FileMode.Create);
                    await video.CopyToAsync(fsVideo);
                    fsVideo.Close();

                    var inputFile = new MediaFile { Filename = VideoPath + ".webm" };
                    var outputFile = new MediaFile { Filename = AudioPath + ".wav" };
                    var conversionOptions = new ConversionOptions
                    {
                        AudioSampleRate = AudioSampleRate.Hz22050//Separating audio from video
                    };
                    using (var engine = new Engine())
                    {
                        engine.Convert(inputFile, outputFile);//, conversionOptions);
                    }

                    SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
                    Grammar gr = new DictationGrammar();
                    sre.LoadGrammar(gr);
                    sre.SetInputToWaveFile(AudioPath + ".wav");
                    sre.BabbleTimeout = new TimeSpan(Int32.MaxValue);
                    sre.InitialSilenceTimeout = new TimeSpan(Int32.MaxValue);
                    sre.EndSilenceTimeout = new TimeSpan(100000000);
                    sre.EndSilenceTimeoutAmbiguous = new TimeSpan(100000000);

                    StringBuilder sb = new StringBuilder();
                    while (true)
                    {
                        try
                        {
                            var recText = sre.Recognize();
                            if (recText == null)
                            {
                                break;
                            }

                            sb.Append(recText.Text);
                        }
                        catch (Exception ex)
                        {
                            //handle exception      
                            //...

                            break;
                        }
                    }
                    var introdata = sb.ToString();
                    
                    var sampleData = new PersonalityML.ModelInput()
                    {
                        Candidate_Details_ID = 5F,
                        Introduction_Video_Data = introdata,
                    };

                    var resumeData = ExtractTextFromPdf(ResumePath + ".pdf");
                    SkillMatch(resumeData, resume.Resume_ID, jobID, resume);
                    var result = PersonalityML.Predict(sampleData);
                    introduction.DISC_Trait = result.PredictedLabel;
                    _db.Introduction.Update(introduction);
                    _db.SaveChanges();
                }

            }
            return Json(HttpStatusCode.BadRequest);
        }


        public bool SendMail(string email, string subject, string body)
        {
            var sysLogin = "alliancerecruitmentteam@gmail.com";
            var sysPass = "eecqfasobfdiaydp";
            var sysAddress = new MailAddress(sysLogin, "Alliance Recruitment Team");

            var receiverAddress = new MailAddress(email);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",  
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(sysLogin, sysPass)
            };

            using (var message = new MailMessage(sysAddress, receiverAddress) { Subject = subject, Body = body })
            {
                message.IsBodyHtml = true;  
                smtp.EnableSsl = true;
                smtp.Send(message);
            }

            return true;
        }

        public static string ExtractTextFromPdf(string path)
        {
            using (PdfReader reader = new PdfReader(path))
            {
                StringBuilder text = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }

                return text.ToString();
            }
        }
        public void SkillMatch(string resumeText, int resumeID, int jobID, TableResume resume)
        {
            IEnumerable<TableSkill> skillObj = _db.Skill.Where(x => x.Is_Deleted == false);
            var jobObj = _db.JobApplication.Find(jobID);
            IEnumerable<TableResponsibility> respObj = _db.Responsibility.Where(x => x.Is_Deleted == false && x.Job_Application_ID == jobID);
            IEnumerable<TableQualification> qualObj = _db.Qualification.Where(x => x.Is_Deleted == false && x.Job_Application_ID == jobID);

            StringBuilder jobDetails = new StringBuilder();
            jobDetails.Append(jobObj.Job_Description);
            foreach (var r in respObj)
            {
                jobDetails.Append(" "+r.Responsibility+" "+r.Description);
            }

            foreach (var q in qualObj)
            {
                jobDetails.Append(" " + q.Qualification + " " + q.Description);
            }
            string jobText = jobDetails.ToString();
            int otherSkills = 0;
            int matchedSkills = 0;
            int jobSkills = 0;
            foreach (var skill in skillObj)
            {
                if (jobText.ToUpper().Contains(skill.Data.ToUpper()))
                    jobSkills++;

                if (resumeText.ToUpper().Contains(skill.Data.ToUpper())) {
                    otherSkills++;
                TableData data = new TableData();
                data.Resume_ID = resumeID;
                data.Data = skill.Data;
                    if (jobText.ToUpper().Contains(skill.Data.ToUpper()))
                    {
                        data.Skill_Matched = true;
                        matchedSkills++;
                        otherSkills--;
                    }
                        
                _db.Data.Add(data);
                };
            }
            double score = Math.Round((((double)matchedSkills / (double)jobSkills) * 80)+(otherSkills*.2), 2);
            resume.Resume_Score =(int)score;

            _db.Resume.Update(resume);
            _db.SaveChanges();
        }


    }
}
