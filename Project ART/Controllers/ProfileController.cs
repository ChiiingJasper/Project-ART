using Microsoft.AspNetCore.Mvc;
using Project_ART.Data;
using Project_ART.Models;
using System.Net;
using System.Text;

namespace Project_ART.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProfileController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var id = HttpContext.Session.GetInt32("Id");
            ViewBag.currentUser = _db.User.SingleOrDefault(x => x.Company_ID == id);
            var userFromDb = _db.User.Find(id);
            return View(userFromDb);
        }

        public IActionResult EditUser()
        {
            var id = HttpContext.Session.GetInt32("Id");
            ViewBag.currentUser = _db.User.SingleOrDefault(x => x.Company_ID == id);

            var userFromDb = _db.User.Find(id);
            return View(userFromDb);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitEditUser()
        {

            var data = Request.Form;
            

                var id = HttpContext.Session.GetInt32("Id");

                TableUser user = _db.User.SingleOrDefault(x => x.Company_ID == id);


                user.First_Name = data["First Name"];
                user.Last_Name = data["Last Name"];
                string MI = data["Middle Initial"];
                user.Middle_Initial = MI.ToUpper().ToCharArray()[0];

                user.Email = data["Email"];
                user.Mobile_Number = data["Mobile Number"];
                if(data["Password"] != "None")
                {
                    user.Password = BCrypt.Net.BCrypt.HashPassword(data["Password"]);
                }
                
                
              

                if (data["Photo"] != "None")
                {
                    string fileName = user.Company_ID + user.Last_Name + "_" + user.First_Name;
                    user.Profile_Pic = fileName + ".png";



                    var photo = Request.Form.Files["Photo"];


                    if (photo != null)
                    {

                        string UploadFolder = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");

                        string PhotoPath = System.IO.Path.Combine(UploadFolder, fileName);


                        FileStream fsPhoto = new FileStream(PhotoPath + ".png", FileMode.Create);
                        await photo.CopyToAsync(fsPhoto);
                        fsPhoto.Close();

                    }
                }

                


                _db.User.Update(user);
                _db.SaveChanges();

            HttpContext.Session.SetString("Name", user.First_Name + " " + user.Last_Name);
            HttpContext.Session.SetString("Picture", user.Profile_Pic);


            return Json(HttpStatusCode.BadRequest);
        }
    }
}
