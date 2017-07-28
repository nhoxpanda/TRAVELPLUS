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
    public class tbl_StaffSalary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("tbl_Staff")]
        public int StaffId { get; set; }
        public decimal? DealSalary { get; set; }
        public decimal? StartSalary { get; set; }
        public decimal? BasicSalary { get; set; }
        public decimal? SubsidySalary { get; set; }
        public decimal? AllowanceSalary { get; set; }
        public decimal? CurSalary { get; set; }
        public string Note { get; set; }

        public tbl_Staff tbl_Staff { get; set; }
        public virtual ICollection<tbl_StaffSalaryDetail> tbl_StaffSalaryDetail { get; set; }
    }
}
