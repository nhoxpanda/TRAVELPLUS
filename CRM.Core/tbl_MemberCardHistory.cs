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
    public class tbl_MemberCardHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CustomerId { get; set; }
        public int MemberCardId { get; set; }
        public Nullable<int> Point { get; set; }
        public int StaffId { get; set; }
        public string Note { get; set; }
        public DateTime CreatedDate { get; set; }
        [DefaultValue(false)]
        public Boolean IsDelete { get; set; }

        [ForeignKey("MemberCardId")]
        public virtual tbl_MemberCard tbl_MemberCard { get; set; }
        [ForeignKey("StaffId")]
        public virtual tbl_Staff tbl_Staff { get; set; }
        [ForeignKey("CustomerId")]
        public virtual tbl_Customer tbl_Customer { get; set; }
    }
}
