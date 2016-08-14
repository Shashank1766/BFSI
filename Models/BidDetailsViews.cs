using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BidKaro.Model.Model;
using BidKaro.Model.CustomModel;

namespace BidKaro.Models
{
    public class BidDetailsView
    {
     
        public BidDetails CurrentBidDetails { get; set; }
        public List<BidDetailsCustomModel> BidDetailsList { get; set; }

    }
}
