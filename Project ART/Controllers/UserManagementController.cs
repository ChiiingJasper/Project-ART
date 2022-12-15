using MediaToolkit.Model;
using MediaToolkit.Options;
using MediaToolkit;
using Microsoft.AspNetCore.Mvc;
using Project_ART.Data;
using Project_ART.Models;
using System.Dynamic;
using System.Net;
using System.Speech.Recognition;
using System.Text;
using System.Net.Mail;

namespace Project_ART.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserManagementController(ApplicationDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            bool deleteFlag = false;
            IEnumerable<TableUser> obj = _db.User.Where(x => x.Is_Deleted == deleteFlag);
            return View(obj);
        }

        public IActionResult ViewUser(int id)
        {
            ViewBag.currentUser = _db.User.SingleOrDefault(x => x.Company_ID == id);
            var userFromDb = _db.User.Find(id);
            return View(userFromDb);
        }

        public IActionResult CreateUser()
        {
           
            return View();
        }


        [HttpGet]
        public IActionResult DeleteUser(int? id)
        {
            var user = _db.User.Find(id);
            user.Is_Deleted = true;
            _db.User.Update(user);
            _db.SaveChanges();

            //ADD LOG
            int? SessionId = _httpContextAccessor.HttpContext.Session.GetInt32("Id");
            DateTime now = DateTime.Now;
            String tableName = "TableUser";
            String logDesc = "Deleted User";
            TableLog tableLog = new TableLog();
            tableLog.Table = tableName;
            tableLog.Table_ID = user.Company_ID;
            tableLog.Description = logDesc;
            tableLog.Date_Time = now.ToString("F");
            tableLog.User_ID = (int)SessionId;
            _db.Log.Add(tableLog);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SubmitUser()
        {

            var data = Request.Form;
            if (Request.Form.Files.Any())
            {


                TableUser user = new TableUser();


                user.First_Name = data["First Name"];
                user.Last_Name = data["Last Name"];
                string MI = data["Middle Initial"];
                user.Middle_Initial = MI.ToUpper().ToCharArray()[0];

                user.Email = data["Email"];
                user.Mobile_Number = data["Mobile Number"];

                const string src = "QWERTYUIOPASDFGHJKLZXCVBNM0123456789";
                int length = 8;
                var sb = new StringBuilder();
                Random RNG = new Random();
                for (var i = 0; i < length; i++)
                {
                    var c = src[RNG.Next(0, src.Length)];
                    sb.Append(c);
                }
                user.Password = BCrypt.Net.BCrypt.HashPassword(sb.ToString());
                if(data["Privilege"] == "0")
                {
                    user.Is_Admin = false;
                }
                else
                {
                    user.Is_Admin = true;
                }

                _db.User.Add(user);
                _db.SaveChanges();



                string fileName = user.Company_ID + user.Last_Name + "_" + user.First_Name;
                user.Profile_Pic = fileName + ".png";



                var photo = Request.Form.Files["Photo"];


                if (photo != null)
                {

                    string UploadFolder = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");

                    string PhotoPath = System.IO.Path.Combine(UploadFolder , fileName);


                    FileStream fsPhoto = new FileStream(PhotoPath + ".png", FileMode.Create);
                    await photo.CopyToAsync(fsPhoto);
                    fsPhoto.Close();

                }
                string subject = "New User";
                string body = "<h3>Hi " + user.First_Name + " " + user.Last_Name + "!<br>" +
                    "You may now login to the Alliance Recruitment Tool, you may use your email to login. <br>" +
                    "Here is your Password:"+sb.ToString()+" <br><br>"+
                    "<br><br>Sincerly Yours, <br>Alliance Recruitment Team</h3>";
                SendMail(user.Email, subject, body);

                _db.User.Update(user);
                _db.SaveChanges();

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

    }
}
