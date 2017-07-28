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
    public class tbl_Bank
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Note { get; set; }
        public DateTime CreateDate { get; set; }
        [DefaultValue(false)]
        public Boolean IsDelete { get; set; }
        public virtual ICollection<tbl_BankDetail> tbl_BankDetail { get; set; }
        public virtual ICollection<tbl_POApproval> tbl_POApproval { get; set; }
    }
}
