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
    public class tbl_POApproval
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string PONumber { get; set; }
        public Nullable<DateTime> Date { get; set; }
        public Nullable<DateTime> Deadline { get; set; }
        public Nullable<int> PaymentMethodId { get; set; }
        public decimal? AmountNumber { get; set; }
        public string AmountWords { get; set; }
        public Nullable<int> CurrencyId { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<int> PartnerId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public string Note { get; set; }
        public Nullable<int> BankId { get; set; }
        public string Requester { get; set; }
        public string Purchasing { get; set; }
        public string ViceDirector { get; set; }
        public string PayableAccoutant { get; set; }
        public string CheifAccountant { get; set; }
        public string ManagingDirector { get; set; }
        public string CustomerName { get; set; }
        public string PartnerName { get; set; }
        public Nullable<int> StaffId { get; set; }
        public DateTime CreateDate { get; set; }
        [DefaultValue(false)]
        public Boolean IsDelete { get; set; }

        [ForeignKey("PaymentMethodId")]
        public virtual tbl_Dictionary tbl_DictionaryPaymentMethod { get; set; }
        [ForeignKey("CurrencyId")]
        public virtual tbl_Dictionary tbl_DictionaryCurrency { get; set; }
        [ForeignKey("StatusId")]
        public virtual tbl_Dictionary tbl_DictionaryStatus { get; set; }
        [ForeignKey("BankId")]
        public virtual tbl_Bank tbl_Bank { get; set; }
        [ForeignKey("PartnerId")]
        public virtual tbl_Partner tbl_Partner { get; set; }
        [ForeignKey("CustomerId")]
        public virtual tbl_Customer tbl_Customer { get; set; }
        [ForeignKey("StaffId")]
        public virtual tbl_Staff tbl_Staff { get; set; }

        public virtual ICollection<tbl_POApprovalDetail> tbl_POApprovalDetail { get; set; }
        public virtual ICollection<tbl_POApprovalHistory> tbl_POApprovalHistory { get; set; }

    }
}
