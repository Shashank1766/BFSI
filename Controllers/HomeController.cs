using BidKaro.Bal.BusinessAccess;
using BidKaro.Common;
using BidKaro.Common.Common;
using BidKaro.Model;
using BidKaro.Model.Model;
using BidKaro.WebCommunicator;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BidKaro.Controllers
{
    public class HomeController : BaseController
    {
        public HomeWebCommunicator ObjHomeWebCommunicator { get; private set; }

        // GET: /Home/

        /// <summary>
        /// Logins this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult LogOff()
        {
            var objSession = Session[SessionNames.SessionClass.ToString()] as SessionClass;
            var userType = 1;
            if (objSession != null)
            {
                using (var bal = new LoginTrackingBal())
                    bal.UpdateLoginOutTime(objSession.UserId,DateTime.UtcNow);
                userType = objSession.LoginUserType;
            }
            Session.RemoveAll();

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            var cookie2 = new HttpCookie("ASP.NET_SessionId", "") { Expires = DateTime.Now.AddYears(-1) };
            Response.Cookies.Add(cookie2);

            // Invalidate the Cache on the Client Side
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetNoStore();


            return RedirectToAction("Login");
        }

        /// <summary>
        /// Validates the mobile.
        /// </summary>
        /// <param name="phonenumber">The phonenumber.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ValidateMobile(string phonenumber)
        {
            var usersBal = new UsersBal();
            var isvalid = usersBal.ValidateMobile(phonenumber);
            return Json(isvalid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Validates the user identifier.
        /// </summary>
        /// <param name="userid">The userid.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ValidateUserId(string userid)
        {
            var usersBal = new UsersBal();
            var isvalid = usersBal.ValidateUser(userid);
            return Json(isvalid, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Validates the email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ValidateEmail(string email)
        {
            var usersBal = new UsersBal();
            var isvalid = usersBal.ValidateEmail(email);
            return Json(isvalid, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Logins the user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult LoginUser(string username, string password)
        {
            var userValid = new Users();
            using (UsersBal userbal = new UsersBal())
            {
                userValid = userbal.CheckUser(username, password);
                if (userValid == null && userValid.Id == 0) return Json(userValid, JsonRequestBehavior.AllowGet);
                using (var bal = new LoginTrackingBal())
                {
                    var loginTrackingVm = new LoginTracking
                    {
                        LoginTimeUTC = DateTime.UtcNow,
                        LoginUserType = 1,
                        IsDeleted = false,
                        IPAddress = Helpers.GetUser_IP(),
                        CreatedBy = userValid.Id,
                        CreatedDateUTC = DateTime.UtcNow,
                        UserID= userValid.Id,
                    };
                    bal.SaveLoginTracking(loginTrackingVm);
                    var userObj = userbal.GetUsersById(userValid.Id);
                    var objSession = Session[SessionNames.SessionClass.ToString()] != null
                                            ? Session[SessionNames.SessionClass.ToString()] as SessionClass
                                            : new SessionClass();
                    objSession.UserName = userObj.UserName;
                    objSession.UserId = userObj.Id;
                    objSession.SelectedCulture = CultureInfo.CurrentCulture.Name;
                    objSession.UserIsAdmin = false;
                    objSession.UserEmail = userObj.EmailId;
                    objSession.RoleName = "";
                    var timeZoneObj = TimeZoneInfo.FindSystemTimeZoneById(TimeZone.CurrentTimeZone.StandardName);
                    objSession.TimeZone = timeZoneObj.BaseUtcOffset.TotalHours.ToString();
                    Session[SessionNames.SessionClass.ToString()] = objSession;
                }
            }
            return Json(userValid.Id, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadExcelFile(HttpPostedFileBase biddingFile)
        {
            var stringStatus = string.Empty;
            var pannumberLink = this.SaveImage(biddingFile);
            var fileExt = Path.GetExtension(biddingFile.FileName);
            ImportExcelToDB(pannumberLink, fileExt, "yes");
            return null;
        }

        private string SaveImage(HttpPostedFileBase file)
        {
            var msgString = string.Empty;
            if (file == null)
            {
                msgString = "Please Upload Your file";
            }
            else if (file.ContentLength > 0)
            {
                var MaxContentLength = 1024 * 1024 * 8; //3 MB
                var AllowedFileExtensions = new[] { ".xls",".xlsx" };

                if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                {
                    msgString = "Please file of type: " + string.Join(", ", AllowedFileExtensions);
                }
                else if (file.ContentLength > MaxContentLength)
                {
                    msgString = "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB";
                }
                else
                {
                    //TO:DO
                    var fileName = Path.GetFileName(file.FileName);
                    Random rd = new Random();
                    var filePath = string.Format(CommonConfig.DocumentsFilePath, Helpers.GetLoggedInUserId());
                    var completePath = Server.MapPath(filePath);
                    if (!Directory.Exists(completePath)) Directory.CreateDirectory(completePath);
                    filePath = filePath + fileName;
                    file.SaveAs(completePath + fileName);
                    msgString = filePath;
                }
            }
            return msgString;
        }

        private void ImportExcelToDB(string FilePath, string Extension, string isHDR)
        {
            string conStr = "";
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, isHDR);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;

            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();

            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();

            //Bind Data to GridView
        }
    }
}