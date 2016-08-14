// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HolidayPlannerController.cs" company="SPadez">
//   OmniHealthcare
// </copyright>
// <summary>
//   The holiday planner controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BidKaro.Controllers
{
    using System.Web.Mvc;

    using Bal.BusinessAccess;
    using Common;
    using Model;
    using Model.CustomModel;
    using Models;
    using System.Web;
    using System.IO;
    using System.Linq;
    using System;
    using System.Collections.Generic;
    using Common.Common;
    using Model.Model;
    using System.Globalization;
    public class UsersController : Controller
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Bind all the Users list
        /// </summary>
        /// <returns>action result with the partial view containing the Users list object</returns>
        [HttpPost]
        public ActionResult BindUsersList()
        {
            // Initialize the Users BAL object
            using (var UsersBal = new UsersBal())
            {
                // Get the facilities list
                var UsersList = UsersBal.GetUsers();

                // Pass the ActionResult with List of UsersViewModel object to Partial View UsersList
                return this.PartialView(PartialViews.UsersList, UsersList);
            }
        }

        /// <summary>
        /// Delete the current Users based on the Users ID passed in the UsersModel
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult DeleteUsers(int id)
        {
            using (var bal = new UsersBal())
            {
                // Get Users model object by current Users ID
                var currentUsers = bal.GetUsersById(id);
                var userId = Helpers.GetLoggedInUserId();

                // Check If Users model is not null
                if (currentUsers != null)
                {
                    currentUsers.IsDeleted = true;

                    // currentUsers.ModifiedBy = userId;
                    // currentUsers.ModifiedDate = DateTime.Now;

                    // Update Operation of current Users
                    int result = bal.SaveUsers(currentUsers);

                    // return deleted ID of current Users as Json Result to the Ajax Call.
                    return this.Json(result);
                }
            }

            // Return the Json result as Action Result back JSON Call Success
            return this.Json(null);
        }

        /// <summary>
        /// Get the details of the current Users in the view model by ID
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult GetUsers(int? id)
        {
            using (var bal = new UsersBal())
            {
                var userid = id ?? Helpers.GetLoggedInUserId();
                // Call the AddUsers Method to Add / Update current Users
                Users currentUsers = bal.GetUsersById(userid);

                // Pass the ActionResult with the current UsersViewModel object as model to PartialView UsersAddEdit
                var viewpath = string.Format("../Users/{0}", PartialViews.MyAccount);
                return this.PartialView(viewpath, currentUsers);
            }
        }

        /// <summary>
        /// Get the details of the Users View in the Model Users such as UsersList, list of
        ///     countries etc.
        /// </summary>
        /// <returns>
        /// returns the actionresult in the form of current object of the Model Users to be passed to View
        ///     Users
        /// </returns>
        public ActionResult UsersMain()
        {
            var UsersView = new UsersView();
            // Initialize the Users BAL object
            var uId = Helpers.GetLoggedInUserId();
            var UsersBal = new UsersBal();
            var userObj = UsersBal.GetUsersById(uId);
            UsersView.CurrentUsers = userObj;
            UsersView.UsersList = UsersBal.GetUsers();
            return View(UsersView);
        }

        /// <summary>
        /// Reset the Users View Model and pass it to UsersAddEdit Partial View.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult ResetUsersForm()
        {
            // Intialize the new object of Users ViewModel
            var UsersViewModel = new Users();

            // Pass the View Model as UsersViewModel to PartialView UsersAddEdit just to update the AddEdit partial view.
            return this.PartialView(PartialViews.UsersAddEdit, UsersViewModel);
        }

        /// <summary>
        /// Add New or Update the Users based on if we pass the Users ID in the UsersViewModel
        ///     object.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// returns the newly added or updated ID of Users row
        /// </returns>
        public ActionResult SaveUsers(Users model)
        {
            // Initialize the newId variable 
            int newId = -1;
            int userId = Helpers.GetLoggedInUserId();

            // Check if Model is not null 
            if (model != null)
            {
                using (var bal = new UsersBal())
                {
                    if (model.Id > 0)
                    {
                        // model.ModifiedBy = userId;
                        // model.ModifiedDate = DateTime.Now;
                    }

                    // Call the AddUsers Method to Add / Update current Users
                    newId = bal.SaveUsers(model);
                }
            }

            return this.Json(newId);
        }

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Register(string title)
        {
            Session.RemoveAll();
            if (!string.IsNullOrEmpty(title))
            {
                ViewBag.result = title;
                return View();
            }
            return View();
        }

        /// <summary>
        /// My account.
        /// </summary>
        /// <returns></returns>
        public ActionResult MyAccount()
        {
            using (var bal = new UsersBal())
            {
                var userid = Helpers.GetLoggedInUserId();
                // Call the AddUsers Method to Add / Update current Users
                Users currentUsers = bal.GetUsersById(userid);
                // Pass the ActionResult with the current UsersViewModel object as model to PartialView UsersAddEdit
                return View(currentUsers);
            }
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(RegisterViewModel model, HttpPostedFileBase pannumber, HttpPostedFileBase Addressproof)
        {
            var stringStatus = string.Empty;
            var encodedResponse = Request.Form["g-Recaptcha-Response"];
            var isCaptchaValid = ReCaptcha.Validate(encodedResponse);
            if (isCaptchaValid)
            {
                var pannumberLink = string.Empty;
                var addressproofLink = string.Empty;
                var pannumberObj = this.SaveImage(pannumber);
                var addressproofOnj = this.SaveImage(Addressproof);
                var panCardsucessObj = pannumberObj.FirstOrDefault(x => x.Key == "Sucess");
                var uploadError = pannumberObj.Any(x => x.Key == "Error");
                if (panCardsucessObj.Value == "false") { pannumberLink = ""; }
                if (panCardsucessObj.Value == "true") { pannumberLink = panCardsucessObj.Value; }

                uploadError = addressproofOnj.Any(x => x.Key == "Error");
                var addressProofsucessObj = pannumberObj.FirstOrDefault(x => x.Key == "Sucess");
                if (addressProofsucessObj.Value == "false") { addressproofLink = ""; }
                if (addressProofsucessObj.Value == "true") { addressproofLink = addressProofsucessObj.Value; }

                if (uploadError) { stringStatus = ViewBag.result = "File Upload error"; }
                var users = new Users()
                {
                    UserName = model.FirstName + " "+ model.LastName,
                    AddressProofLink = addressproofLink,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MobileNumber = model.Phone,
                    PanNumberLink = pannumberLink,
                    CityID = model.City,
                    StateID = model.State,
                    EmailId = model.Email,
                    Address= model.Address,
                    PanNumber = model.PanNumber,
                    Password = EncryptDecrypt.Encrypt(EncryptDecrypt.GeneratePasswordResetToken(10, false)),
                    CreatedBy = null,
                    CreatedDate = DateTime.UtcNow,
                    IsDeleted = false,
                    Approved = false,
                    Enabled = false
                };

                var userBal = new UsersBal();
                userBal.SaveUsers(users);
                stringStatus= ViewBag.result = "Record Inserted Successfully!";
                return View();
            }
            stringStatus = ViewBag.result = "Captcha error!";
            return View();
        }

        /// <summary>
        /// Saves the image.
        /// </summary>
        /// <param name="file">The pannumber.</param>
        /// <returns></returns>
        private Dictionary<string,string> SaveImage(HttpPostedFileBase file)
        {
            var msgString = new Dictionary<string, string> ();
            try
            {
                if (file == null)
                {
                    msgString.Add("Sucess", "false");
                }
                else if (file.ContentLength > 0)
                {
                    var MaxContentLength = 1024 * 1024 * 8; //3 MB
                    var AllowedFileExtensions = new[] { ".pdf" };

                    if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                    {
                        msgString.Add("Sucess", "false");
                        msgString.Add("Msg", "Please file of type: " + string.Join(", ", AllowedFileExtensions));
                    }
                    else if (file.ContentLength > MaxContentLength)
                    {
                        msgString.Add("Sucess", "false");
                        msgString.Add("Msg", "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");
                    }
                    else
                    {
                        //TO:DO
                        var fileName = Path.GetFileName(file.FileName);
                        Random rd = new Random();
                        var filePath = string.Format(CommonConfig.DocumentsFilePath, rd.Next(Int32.MaxValue));
                        var completePath = Server.MapPath(filePath);
                        if (!Directory.Exists(completePath)) Directory.CreateDirectory(completePath);
                        filePath = filePath + fileName;
                        file.SaveAs(completePath + fileName);
                        msgString.Add("Sucess", "true");
                        msgString.Add("Path", filePath);
                    }

                }
            }
            catch (Exception ex)
            {
                msgString.Add("Sucess", "false");
                msgString.Add("Error", "Unable to uplaod Image");
            }
            return msgString;
        }

        #endregion
    }
}
