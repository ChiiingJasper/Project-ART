using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Project_ART.Data;
using Project_ART.Models;
using System.Dynamic;
using System.Text;
using System.Net;
using System.Drawing.Imaging;
using System;
using System.Speech.Recognition;
using System.Speech.AudioFormat;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;
namespace Project_ART.Controllers
{
    public class CandidatesController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CandidatesController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            ViewBag.Scores = _db.Weighting.Find(1);
            bool deleteFlag = false;
            String tableName = "TableCandidate";
            IEnumerable<TableCandidate> obj = _db.Candidate.Where(x => x.Is_Deleted == deleteFlag);


            var Candidates =
                             from candidate in _db.Candidate.Where(x => x.Is_Deleted == false)
                             join job in _db.JobApplication.Where(x => x.Is_Deleted == false) on candidate.Job_Application_ID equals job.Job_Application_ID
                             join resume in _db.Resume.Where(x => x.Is_Deleted == false) on candidate.Resume_ID equals resume.Resume_ID
                             join assessment in _db.Assessment on candidate.Assessment_ID equals assessment.Assessment_ID
                             join exam in _db.Exam on assessment.Assessment_ID equals exam.Exam_ID
                             join interview in _db.Interview on assessment.Assessment_ID equals interview.Interview_ID
                             join introduction in _db.Introduction on candidate.Introduction_ID equals introduction.Introduction_ID
                             select new
                             {
                                 Candidate_ID = candidate.Candidate_ID,
                                 Job = job.Job,
                                 First_Name = candidate.First_Name,
                                 Last_Name = candidate.Last_Name,
                                 Middle_Initial = candidate.Middle_Initital,
                                 Email = candidate.Email,
                                 DISC = introduction.DISC_Trait,
                                 Resume_Score = resume.Resume_Score,
                                 Photo = candidate.Photo,
                                 Exam = exam.Exam_Score,
                                 Interview = interview.Interview_Score,
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
            ViewBag.Candidate = Candidates.ToList();

            return View();
        }

        public IActionResult ViewCandidate(int? id)
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

        [HttpGet]
        public IActionResult DeleteCandidate(int? id)
        {
            var candidate = _db.Candidate.Find(id);
            candidate.Is_Deleted = true;
            _db.Candidate.Update(candidate);
            _db.SaveChanges();

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableCandidate";
            String logDesc = "Deleted Candidate";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = candidate.Candidate_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult UploadCandidate()
        {
            ViewBag.Jobs = _db.JobApplication.Where(x => x.Is_Deleted == false && x.Is_Open == true);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitAssessment()
        {

            var data = Request.Form;

            int candidateID = Int32.Parse(data["candidateID"]);
            var score = Int32.Parse(data["score"]);

            var candidate = _db.Candidate.Find(candidateID);
            var assessment = _db.Assessment.Find(candidate.Assessment_ID);
            var exam = _db.Exam.Find(assessment.Exam_ID);

            exam.Exam_Score = score;
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

        [HttpPost]
        public async Task<IActionResult> SaveCandidateDetails()
        {

            var data = Request.Form;
            if (Request.Form.Files.Any())
            {


                TableResume resume = new TableResume();
                TableIntroduction introduction = new TableIntroduction();
                TableExam exam = new TableExam { Exam_Score = 0 };
                TableInterview interview = new TableInterview { Interview_Score = 0 };

                TableAssessment assessment = new TableAssessment { Exam = exam, Interview = interview };

                TableCandidate candidate = new TableCandidate { Resume = resume, Introduction = introduction, Assessment = assessment };
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
                candidate.Email_Confirmed = true;
                _db.Candidate.Add(candidate);
                _db.SaveChanges();

                TableStatus status = new TableStatus();
                status.Candidate = candidate;
                _db.Status.Add(status);
                _db.SaveChanges();

                string fileName = candidate.Candidate_ID + candidate.Last_Name + "_" + candidate.First_Name;
                candidate.Photo = fileName + ".png";
                introduction.Introduction_Video = fileName + ".webm";
                resume.Resume = fileName + ".pdf";
                _db.Candidate.Update(candidate);


                

                var photo = Request.Form.Files["Photo"];
                var cv = Request.Form.Files["Resume"];
                var video = Request.Form.Files["Introduction Video"];

                if (photo != null && cv != null && video != null)
                {

                    string UploadFolder = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedFiles");

                    string PhotoPath = System.IO.Path.Combine(UploadFolder + "/Photo", fileName);
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
                jobDetails.Append(" " + r.Responsibility + " " + r.Description);
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

                if (resumeText.ToUpper().Contains(skill.Data.ToUpper()))
                {
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
            double score = Math.Round((((double)matchedSkills / (double)jobSkills) * 80) + (otherSkills * .2), 2);
            resume.Resume_Score = (int)score;

            _db.Resume.Update(resume);
            _db.SaveChanges();
        }


        public async Task<IActionResult> SubmitPersonalityScore()
        {

            var data = Request.Form;
            var score = Int32.Parse(data["score"]);

            var weighting = _db.Weighting.Find(1);


            weighting.Personality = score;
            _db.Weighting.Update(weighting);
            _db.SaveChanges();


            return Json(HttpStatusCode.BadRequest);
        }

        public async Task<IActionResult> SubmitResumeScore()
        {

            var data = Request.Form;
            var score = Int32.Parse(data["score"]);

            var weighting = _db.Weighting.Find(1);


            weighting.Resume = score;
            _db.Weighting.Update(weighting);
            _db.SaveChanges();


            return Json(HttpStatusCode.BadRequest);
        }

        public async Task<IActionResult> SubmitExamScore()
        {

            var data = Request.Form;
            var score = Int32.Parse(data["score"]);

            var weighting = _db.Weighting.Find(1);


            weighting.Exam = score;
            _db.Weighting.Update(weighting);
            _db.SaveChanges();


            return Json(HttpStatusCode.BadRequest);
        }

        public async Task<IActionResult> SubmitInterviewScore()
        {

            var data = Request.Form;
            var score = Int32.Parse(data["score"]);

            var weighting = _db.Weighting.Find(1);


            weighting.Interview = score;
            _db.Weighting.Update(weighting);
            _db.SaveChanges();


            return Json(HttpStatusCode.BadRequest);
        }
    }
}
