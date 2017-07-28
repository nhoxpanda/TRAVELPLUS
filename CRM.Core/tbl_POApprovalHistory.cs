using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core
{
    public class tbl_POApprovalHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        [ForeignKey("tbl_Staff")]
        public Nullable<int> StaffId { get; set; }
        public string Note { get; set; }
        [ForeignKey("tbl_POApprovalPermission")]
        public Nullable<int> ApprovalPermissionId { get; set; }
        [ForeignKey("tbl_POApproval")]
        public Nullable<int> POApprovalId { get; set; }
        [DefaultValue(false)]
        public Boolean IsAccept { get; set; }
        [DefaultValue(0)]
        public int Times { get; set; }

        public virtual tbl_Staff tbl_Staff { get; set; }
        public virtual tbl_POApproval tbl_POApproval { get; set; }
        public virtual tbl_POApprovalPermission tbl_POApprovalPermission { get; set; }

    }
}
