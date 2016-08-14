namespace BidKaro.Controllers
{
    using System.Web.Mvc;
    using BidKaro.Model;
    using BidKaro.Models;
    using Bal.BusinessAccess;
    /// <summary>
    /// AuditLog controller.
    /// </summary>
    public class AuditLogController : Controller
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Bind all the AuditLog list
        /// </summary>
        /// <returns>action result with the partial view containing the AuditLog list object</returns>
        [HttpPost]
        public ActionResult BindAuditLogList()
        {
            // Initialize the AuditLog BAL object
            using (var AuditLogBal = new AuditLogBal())
            {
                // Get the facilities list
                var AuditLogList = AuditLogBal.GetAuditLog();

                // Pass the ActionResult with List of AuditLogViewModel object to Partial View AuditLogList
                return null;// this.PartialView(PartialViews.AuditLogList, AuditLogList);
            }
        }

        /// <summary>
        /// Delete the current AuditLog based on the AuditLog ID passed in the AuditLogModel
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult DeleteAuditLog(int id)
        {
            using (var bal = new AuditLogBal())
            {
                // Get AuditLog model object by current AuditLog ID
                var currentAuditLog = bal.GetAuditLogById(id);
                var userId = 0;//Helpers.GetLoggedInUserId();

                // Check If AuditLog model is not null
                if (currentAuditLog != null)
                {

                    // currentAuditLog.ModifiedBy = userId;
                    // currentAuditLog.ModifiedDate = DateTime.Now;

                    // Update Operation of current AuditLog
                    int result = bal.SaveAuditLog(currentAuditLog);

                    // return deleted ID of current AuditLog as Json Result to the Ajax Call.
                    return this.Json(result);
                }
            }

            // Return the Json result as Action Result back JSON Call Success
            return this.Json(null);
        }

        /// <summary>
        /// Get the details of the current AuditLog in the view model by ID
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult GetAuditLog(int id)
        {
            using (var bal = new AuditLogBal())
            {
                // Call the AddAuditLog Method to Add / Update current AuditLog
                AuditLog currentAuditLog = bal.GetAuditLogById(id);

                // Pass the ActionResult with the current AuditLogViewModel object as model to PartialView AuditLogAddEdit
                return null;// this.PartialView(PartialViews.AuditLogAddEdit, currentAuditLog);
            }
        }

        /// <summary>
        /// Get the details of the AuditLog View in the Model AuditLog such as AuditLogList, list of
        ///     countries etc.
        /// </summary>
        /// <returns>
        /// returns the actionresult in the form of current object of the Model AuditLog to be passed to View
        ///     AuditLog
        /// </returns>
        public ActionResult AuditLogMain()
        {
            // Initialize the AuditLog BAL object
            var AuditLogBal = new AuditLogBal();

            // Get the Entity list
            var AuditLogList = AuditLogBal.GetAuditLog();

            // Intialize the View Model i.e. AuditLogView which is binded to Main View Index.cshtml under AuditLog
            var AuditLogView = new AuditLogView
                                         {
                                             AuditLogList = AuditLogList, 
                                             CurrentAuditLog = new AuditLog()
                                         };

            // Pass the View Model in ActionResult to View AuditLog
            return View(AuditLogView);
        }

        /// <summary>
        /// Reset the AuditLog View Model and pass it to AuditLogAddEdit Partial View.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult ResetAuditLogForm()
        {
            // Intialize the new object of AuditLog ViewModel
            var AuditLogViewModel = new AuditLog();

            // Pass the View Model as AuditLogViewModel to PartialView AuditLogAddEdit just to update the AddEdit partial view.
            return null;// this.PartialView(PartialViews.AuditLogAddEdit, AuditLogViewModel);
        }

        /// <summary>
        /// Add New or Update the AuditLog based on if we pass the AuditLog ID in the AuditLogViewModel
        ///     object.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// returns the newly added or updated ID of AuditLog row
        /// </returns>
        public ActionResult SaveAuditLog(AuditLog model)
        {
            // Initialize the newId variable 
            int newId = -1;
            int userId = 0;// Helpers.GetLoggedInUserId();

            // Check if Model is not null 
            if (model != null)
            {
                using (var bal = new AuditLogBal())
                {
                    if (model.ID > 0)
                    {
                        // model.ModifiedBy = userId;
                        // model.ModifiedDate = DateTime.Now;
                    }

                    // Call the AddAuditLog Method to Add / Update current AuditLog
                    newId = bal.SaveAuditLog(model);
                }
            }

            return this.Json(newId);
        }

        #endregion
    }
}
