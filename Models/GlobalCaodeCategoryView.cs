using BidKaro.Model;
using BidKaro.Model.CustomModel;
using BidKaro.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BidKaro.Models
{
    public class GlobalCaodeCategoryView
    {
        public GlobalCodeCategory CurrentModel { get; set; }
        public List<GlobalCodeCategoryCustomModel> ModelList { get; set; }
    }
}