using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Core;

namespace TRAVELPLUS.Models
{
    public class POApprovalViewModel
    {
        public tbl_POApproval tbl_POApproval { get; set; }
        public List<tbl_POApprovalDetail> tbl_POApprovalDetail { get; set; }
    }
}