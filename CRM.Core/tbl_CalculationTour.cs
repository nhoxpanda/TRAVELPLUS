using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core
{
    public class tbl_TourCalculation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int TourId { get; set; }
        public int PartnerId { get; set; }
        public int ServiceId { get; set; }
        public DateTime Date { get; set; }
        public decimal? Price { get; set; }
        public string Note { get; set; }
        public DateTime CreateDate { get; set; }
        public int StaffId { get; set; }
        public Nullable<int> CurrencyId { get; set; }

        [ForeignKey("CurrencyId")]
        public virtual tbl_Dictionary tbl_Dictionary { get; set; }
        [ForeignKey("TourId")]
        public virtual tbl_Tour tbl_Tour { get; set; }
        [ForeignKey("PartnerId")]
        public virtual tbl_Partner tbl_Partner { get; set; }
        [ForeignKey("StaffId")]
        public virtual tbl_Staff tbl_Staff { get; set; }
    }
}
