// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommonConfig.cs" company="BidKaro">
//   Shashank
// </copyright>
// <summary>
//   TODO The partial views.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BidKaro.Common
{
    using System.Configuration;
    using System.Globalization;
    /// <summary>
    /// TODO The partial views.
    /// </summary>
    public static class PartialViews
    {
        /// <summary>
        /// TODO The cpt codes list.
        /// </summary>
        public static readonly string CPTCodesList = string.Format("{0}/_CPTCodesList", CommonConfig.UserControlPath);

        public static readonly string UsersList = string.Format("{0}/_UsersList", CommonConfig.UserControlPath);
        public static readonly string UsersAddEdit = string.Format("{0}/_UsersAddEdit", CommonConfig.UserControlPath);

        public static readonly string MyAccount = string.Format("{0}/_MyAccount", CommonConfig.UserControlPath);

        public static readonly string BidDetailsList = string.Format("{0}/_BidDetailsList", CommonConfig.UserControlPath);
        public static readonly string BidDetailsAddEdit = string.Format("{0}/_BidDetailsAddEdit", CommonConfig.UserControlPath);
        public static readonly string ListingVIew = string.Format("{0}/_ListingVIew", CommonConfig.UserControlPath); 
    }

    /// <summary>
    /// TODO The common config.
    /// </summary>
    public class CommonConfig
    {
        /// <summary>
        /// Gets the user control path.
        /// </summary>
        public static string UserControlPath
        {
            get
            {
                return ConfigurationManager.AppSettings["UserControlsPath"];
            }
        }

        public static string ServiceUrl
        {
            get { return ConfigurationManager.AppSettings["serviceURL"].ToString(CultureInfo.InvariantCulture); }
        }

        public static string WebApiUrl
        {
            get { return ConfigurationManager.AppSettings["WebAPIURL"].ToString(CultureInfo.InvariantCulture); }
        }

        public static string DefaultLanguage
        {
            get { return "en-US"; }
        }

        public static string DocumentsFilePath
        {
            get
            {
                return "\\Content\\Documents\\{0}\\";
            }
        }

        public static string RemittanceAdviceXmlFilePath
        {
            get
            {
                return ConfigurationManager.AppSettings["RemittanceAdviceXmlFilePath"];
            }
        }
    }
}
