using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core
{
    public class tbl_ReceiptBillPeriod
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public decimal? Price { get; set; }
        public int Period { get; set; }
        public string Note { get; set; }
        [ForeignKey("tbl_ReceiptBill")]
        public int ReceiptId { get; set; }
        [ForeignKey("tbl_Dictionary")]
        public int PaymentMethodId { get; set; }

        public virtual tbl_ReceiptBill tbl_ReceiptBill { get; set; }
        public virtual tbl_Dictionary tbl_Dictionary { get; set; }
    }
}
