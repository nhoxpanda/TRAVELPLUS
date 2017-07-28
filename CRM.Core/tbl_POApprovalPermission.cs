using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core
{
    public class tbl_POApprovalPermission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Level { get; set; }
        public int PositionPOId { get; set; }
        public string ListStaffId { get; set; }

        [ForeignKey("PositionPOId")]
        public virtual tbl_Dictionary tbl_Dictionary { get; set; }
        public virtual ICollection<tbl_POApprovalHistory> tbl_POApprovalHistory { get; set; }
    }
}
