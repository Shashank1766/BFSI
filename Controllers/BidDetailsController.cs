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
    using System.Collections.Generic;
    using System.Web.Mvc;

    using BidKaro.Bal.BusinessAccess;
    using BidKaro.Common;
    using BidKaro.Model;
    using BidKaro.Model.CustomModel;
    using BidKaro.Models;
    using Model.Model;
    /// <summary>
    /// BidDetails controller.
    /// </summary>
    public class BidDetailsController : Controller
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Bind all the BidDetails list
        /// </summary>
        /// <returns>action result with the partial view containing the BidDetails list object</returns>
        [HttpPost]
        public ActionResult BindBidDetailsList()
        {
            // Initialize the BidDetails BAL object
            using (var BidDetailsBal = new BidDetailsBal())
            {
                // Get the facilities list
                var BidDetailsList = BidDetailsBal.GetBidDetails();

                // Pass the ActionResult with List of BidDetailsViewModel object to Partial View BidDetailsList
                return this.PartialView(PartialViews.BidDetailsList, BidDetailsList);
            }
        }

        /// <summary>
        /// Delete the current BidDetails based on the BidDetails ID passed in the BidDetailsModel
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult DeleteBidDetails(int id)
        {
            using (var bal = new BidDetailsBal())
            {
                // Get BidDetails model object by current BidDetails ID
                var currentBidDetails = bal.GetBidDetailsById(id);
                var userId = Helpers.GetLoggedInUserId();

                // Check If BidDetails model is not null
                if (currentBidDetails != null)
                {
                    currentBidDetails.IsActive = false;

                    // currentBidDetails.ModifiedBy = userId;
                    // currentBidDetails.ModifiedDate = DateTime.Now;

                    // Update Operation of current BidDetails
                    int result = bal.SaveBidDetails(currentBidDetails);

                    // return deleted ID of current BidDetails as Json Result to the Ajax Call.
                    return this.Json(result);
                }
            }

            // Return the Json result as Action Result back JSON Call Success
            return this.Json(null);
        }

        /// <summary>
        /// Get the details of the current BidDetails in the view model by ID
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult GetBidDetails(int id)
        {
            using (var bal = new BidDetailsBal())
            {
                // Call the AddBidDetails Method to Add / Update current BidDetails
                BidDetails currentBidDetails = bal.GetBidDetailsById(id);

                // Pass the ActionResult with the current BidDetailsViewModel object as model to PartialView BidDetailsAddEdit
                return this.PartialView(PartialViews.BidDetailsAddEdit, currentBidDetails);
            }
        }

        /// <summary>
        /// Get the details of the BidDetails View in the Model BidDetails such as BidDetailsList, list of
        ///     countries etc.
        /// </summary>
        /// <returns>
        /// returns the actionresult in the form of current object of the Model BidDetails to be passed to View
        ///     BidDetails
        /// </returns>
        public ActionResult BidDetailsMain()
        {
            // Initialize the BidDetails BAL object
            var BidDetailsBal = new BidDetailsBal();

            // Get the Entity list
            var BidDetailsList = BidDetailsBal.GetBidDetails();

            // Intialize the View Model i.e. BidDetailsView which is binded to Main View Index.cshtml under BidDetails
            var BidDetailsView = new BidDetailsView
                                         {
                                             BidDetailsList = BidDetailsList, 
                                             CurrentBidDetails = new BidDetails()
                                         };

            // Pass the View Model in ActionResult to View BidDetails
            return View(BidDetailsView);
        }

        /// <summary>
        /// Reset the BidDetails View Model and pass it to BidDetailsAddEdit Partial View.
        /// </summary>
        /// <returns>
        /// The <see cref="ActionResult" />.
        /// </returns>
        public ActionResult ResetBidDetailsForm()
        {
            // Intialize the new object of BidDetails ViewModel
            var BidDetailsViewModel = new BidDetails();

            // Pass the View Model as BidDetailsViewModel to PartialView BidDetailsAddEdit just to update the AddEdit partial view.
            return this.PartialView(PartialViews.BidDetailsAddEdit, BidDetailsViewModel);
        }

        /// <summary>
        /// Add New or Update the BidDetails based on if we pass the BidDetails ID in the BidDetailsViewModel
        ///     object.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// returns the newly added or updated ID of BidDetails row
        /// </returns>
        public ActionResult SaveBidDetails(BidDetails model)
        {
            // Initialize the newId variable 
            int newId = -1;
            int userId = Helpers.GetLoggedInUserId();

            // Check if Model is not null 
            if (model != null)
            {
                using (var bal = new BidDetailsBal())
                {
                    if (model.Id > 0)
                    {
                        // model.ModifiedBy = userId;
                        // model.ModifiedDate = DateTime.Now;
                    }

                    // Call the AddBidDetails Method to Add / Update current BidDetails
                    newId = bal.SaveBidDetails(model);
                }
            }

            return this.Json(newId);
        }

        #endregion
    }
}
