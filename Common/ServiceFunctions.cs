using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BidKaro.Common
{
    public class ServiceFunctions
    {
        private static readonly string ServiceUrl = CommonConfig.ServiceUrl;
        //private static readonly string WebApiUrl = CommonConfig.WebApiUrl;

        public static readonly string Logout = ServiceUrl + "Logout";
    }
}