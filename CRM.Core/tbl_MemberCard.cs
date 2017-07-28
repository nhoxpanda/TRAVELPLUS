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
    public class tbl_MemberCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public decimal? Percent { get; set; }
        [DefaultValue(false)]
        public Boolean IsDelete { get; set; }

        public virtual ICollection<tbl_Customer> tbl_Customer { get; set; }
        public virtual ICollection<tbl_MemberCardHistory> tbl_MemberCardHistory { get; set; }
    }
}
