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
    public class tbl_ReceiptBill
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<int> ContractId { get; set; }
        public Nullable<int> TourId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public string Submitter { get; set; }
        public string Address { get; set; }
        public string Reason { get; set; }
        public string OriginalDocument { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public Nullable<int> CurrencyId { get; set; }
        public Nullable<int> PaymentMethodId { get; set; }
        public Nullable<double> ExchangeRate { get; set; }
        public Nullable<int> StaffReceiveId { get; set; }
        public Nullable<int> StaffId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string FileName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        [DefaultValue(false)]
        public bool IsDelete { get; set; }
        public Nullable<int> StatusId { get; set; }
        public string Note { get; set; }

        [ForeignKey("ContractId")]
        public virtual tbl_Contract tbl_Contract { get; set; }
        [ForeignKey("TourId")]
        public virtual tbl_Tour tbl_Tour { get; set; }
        [ForeignKey("CustomerId")]
        public virtual tbl_Customer tbl_Customer { get; set; }
        [ForeignKey("CurrencyId")]
        public virtual tbl_Dictionary tbl_DictionaryCurrency { get; set; }
        [ForeignKey("StaffReceiveId")]
        public virtual tbl_Staff tbl_StaffReceive { get; set; }
        [ForeignKey("StaffId")]
        public virtual tbl_Staff tbl_Staff { get; set; }
        [ForeignKey("StatusId")]
        public virtual tbl_Dictionary tbl_DictionaryStatus { get; set; }
        public virtual ICollection<tbl_ReceiptBillPeriod> tbl_ReceiptBillPeriod { get; set; }
    }
}
