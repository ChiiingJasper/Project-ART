using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Project_ART.Controllers
{
    public class UploadDetailsController : Controller
    {
        public IActionResult Video()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveRecoredFile()
        {
            if (Request.Form.Files.Any())
            {
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

        public IActionResult Resume()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UploadResume(IFormFile File1)
        {
            if (File1 != null)
            {
                string FileName = File1.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UploadedFiles/Resume", FileName);

                var stream = new FileStream(path, FileMode.Create);
                File1.CopyToAsync(stream);

                string url = "/files/" + FileName;
            }


            return RedirectToAction("Resume", "UploadDetails");
        }
    }
}
