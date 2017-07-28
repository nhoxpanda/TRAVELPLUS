using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core
{
    public class tbl_PaymentBillPeriod
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public decimal? Price { get; set; }
        public int Period { get; set; }
        public string Note { get; set; }
        [ForeignKey("tbl_PaymentBill")]
        public int PaymentId { get; set; }
        [ForeignKey("tbl_Dictionary")]
        public int PaymentMethodId { get; set; }

        public virtual tbl_PaymentBill tbl_PaymentBill { get; set; }
        public virtual tbl_Dictionary tbl_Dictionary { get; set; }
    }
}
