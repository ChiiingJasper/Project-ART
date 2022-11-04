﻿using Project_ART.Data;
using Project_ART.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_ART.Controllers
{
    public class TableCandidateController : Controller
    {
        private readonly ApplicationDbContext _db;

        public TableCandidateController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<TableCandidate> objTableCandidateList = _db.Candidate;
            return View(objTableCandidateList);
        }

        /*public IActionResult CreateCandidate()
        {
            ViewBag.Applications = GetApplications();
            ViewBag.Assessments = GetAssessments();
            ViewBag.Users = GetUsers();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCandidate(TableCandidate obj)
        {
            _db.Candidate.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }*/

        public IActionResult UpdateCandidate(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var candidateFromDb = _db.Candidate.Find(id);

            if (candidateFromDb == null)
            {
                return NotFound();
            }

            ViewBag.Applications = GetApplications();
            ViewBag.Assessments = GetAssessments();
            ViewBag.Resumes = GetResume();
            ViewBag.Introductions = GetIntroduction();
            return View(candidateFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCandidate(TableCandidate obj)
        {
            _db.Candidate.Update(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult DeleteCandidate(int? id)
        {
            var candidateFromDb = _db.Candidate.Find(id);
            _db.Candidate.Remove(candidateFromDb);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //TO GET LISTS
        private List<SelectListItem> GetApplications()
        {
            var lstApplications = new List<SelectListItem>();
            foreach (var item in _db.JobApplication)
            {
                lstApplications.Add(new SelectListItem()
                {
                    Value = item.Job_Application_ID.ToString(),
                    Text = item.Job_Application_ID.ToString()
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Application----"

            };

            lstApplications.Insert(0, defItem);

            return lstApplications;
        }

        private List<SelectListItem> GetAssessments()
        {
            var lstAssessments = new List<SelectListItem>();
            foreach (var item in _db.Assessment)
            {
                lstAssessments.Add(new SelectListItem()
                {
                    Value = item.Assessment_ID.ToString(),
                    //Text = item.Date_Assessed.ToString()
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select Assessment----"

            };

            lstAssessments.Insert(0, defItem);

            return lstAssessments;
        }

        private List<SelectListItem> GetResume()
        {
            var lstResume = new List<SelectListItem>();
            foreach (var item in _db.Resume)
            {
                lstResume.Add(new SelectListItem()
                {
                    Value = item.Resume_ID.ToString(),
                    Text = item.Resume
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select User----"

            };

            lstResume.Insert(0, defItem);

            return lstResume;
        }

        private List<SelectListItem> GetIntroduction()
        {
            var lstIntro = new List<SelectListItem>();
            foreach (var item in _db.Introduction)
            {
                lstIntro.Add(new SelectListItem()
                {
                    Value = item.Introduction_ID.ToString(),
                    Text = item.Introduction_Video
                });
            }

            var defItem = new SelectListItem()
            {
                Value = "",
                Text = "----Select User----"

            };

            lstIntro.Insert(0, defItem);

            return lstIntro;
        }
    }
}
