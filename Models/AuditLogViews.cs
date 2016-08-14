using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BidKaro.Model.CustomModel;
using BidKaro.Model;

namespace BidKaro.Models
{
    public class AuditLogView
    {
     
        public AuditLog CurrentAuditLog { get; set; }
        public List<AuditLogCustomModel> AuditLogList { get; set; }

    }
}
