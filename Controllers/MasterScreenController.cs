using BidKaro.Bal.BusinessAccess;
using BidKaro.Common;
using BidKaro.Model.CustomModel;
using BidKaro.Model.Model;
using BidKaro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BidKaro.Controllers
{
    public class MasterScreenController : BaseController
    {
        // GET: MasterScreen
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var globalcodeCategoryModelview = new GlobalCaodeCategoryView();
            using (var globalCodeCategoryBal = new GlobalCodeCategoryBal())
            {
                globalcodeCategoryModelview.ModelList =  globalCodeCategoryBal.GetGlobalCodeCategoryList();
                globalcodeCategoryModelview.CurrentModel = new GlobalCodeCategory();
            }
            return View(globalcodeCategoryModelview);
        }

        /// <summary>
        /// Adds the global code category.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public ActionResult AddGlobalCodeCategory(string name, string value)
        {
            var listToreturn = new List<GlobalCodeCategoryCustomModel>();
            using (var globalCodeCategoryBal = new GlobalCodeCategoryBal())
            {
                var gloablcodeCategoryModel = new GlobalCodeCategory()
                {
                    CreatedBy =Helpers.GetLoggedInUserId(),
                    GlobalCodeCategoryName= name,
                    GlobalCodeCategoryValue = value,
                    CreatedDate = DateTime.UtcNow
                };
                listToreturn = globalCodeCategoryBal.SaveGlobalCodeCategory(gloablcodeCategoryModel);
            }
            return this.PartialView(PartialViews.ListingVIew, listToreturn);
        }

    }
}