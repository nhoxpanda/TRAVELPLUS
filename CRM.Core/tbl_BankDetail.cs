using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core
{
    public class tbl_BankDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Nullable<int> StaffId { get; set; }
        public Nullable<int> PartnerId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public Nullable<int> BankId { get; set; }
        public string Note { get; set; }
        public DateTime CreateDate { get; set; }

        [ForeignKey("StaffId")]
        public virtual tbl_Staff tbl_Staff { get; set; }
        [ForeignKey("PartnerId")]
        public virtual tbl_Partner tbl_Partner { get; set; }
        [ForeignKey("CustomerId")]
        public virtual tbl_Customer tbl_Customer { get; set; }
        [ForeignKey("BankId")]
        public virtual tbl_Bank tbl_Bank { get; set; }
    }
}
