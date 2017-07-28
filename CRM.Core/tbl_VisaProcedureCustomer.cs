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
    public class tbl_VisaProcedureCustomer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> StaffId { get; set; }
        public Nullable<int> VisaProcedureId { get; set; }
        public Nullable <int> VisaId { get; set; }

        [ForeignKey("VisaId")]
        public virtual tbl_CustomerVisa tbl_CustomerVisa { get; set; }
        [ForeignKey("CustomerId")]
        public virtual tbl_Customer tbl_Customer { get; set; }
        [ForeignKey("StaffId")]
        public virtual tbl_Staff tbl_Staff { get; set; }
        [ForeignKey("VisaProcedureId")]
        public virtual tbl_VisaProcedure tbl_VisaProcedure { get; set; }
    }
}
