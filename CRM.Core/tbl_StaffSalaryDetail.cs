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
    public class tbl_StaffSalaryDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("tbl_StaffSalary")]
        public Nullable<int> StaffSalaryId { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ApplyDate { get; set; }
        public decimal? Value { get; set; }
        public decimal? SalaryAfterChange { get; set; }
        [DefaultValue(true)]
        public Boolean IsIncrease { get; set; }

        public tbl_StaffSalary tbl_StaffSalary { get; set; }
    }
}
