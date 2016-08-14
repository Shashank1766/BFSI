using BidKaro.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BidKaro.Controllers
{
    public class BaseController : Controller
    {
        //public ISessionWrapper SessionWrapper { get; set; }
        public BaseController()
        {
            //var SessionWrapper = new HttpContextSessionWrapper();
        }

        public string BrowserLanguage()
        {
            var languages = HttpContext.Request.UserLanguages;
            var browserLanguage = CommonConfig.DefaultLanguage;
            if (languages != null && languages.Length > 0)
            {
                browserLanguage = languages[0];
                #region look for the direct Match with macrolanguage if direct match not found look for the macrolanguage
                if (browserLanguage == "no-nn" || browserLanguage == "no-nb" || browserLanguage == "nb" ||
                    browserLanguage == "no" || browserLanguage == "nn")
                {
                    browserLanguage = "nb-NO";
                }
                if (browserLanguage == "en-us" || browserLanguage == "en")
                {
                    browserLanguage = CommonConfig.DefaultLanguage;
                }
                #endregion
                return browserLanguage;
            }
            return browserLanguage;
        }
    }
}