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
    public class tbl_Tags
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Tag { get; set; }
        public int? ParentId { get; set; }
        public string ISOCode { get; set; }
        public int? TypeTag { get; set; }
        [DefaultValue(false)]
        public Boolean IsDelete { get; set; }

        public virtual ICollection<tbl_VisaInfomation> tbl_VisaInfomation { get; set; }
        public virtual ICollection<tbl_CustomerContactVisa> tbl_CustomerContactVisa { get; set; }
        public virtual ICollection<tbl_CustomerVisa> tbl_CustomerVisa { get; set; }
        public virtual ICollection<tbl_StaffVisa> tbl_StaffVisa { get; set; }

        public virtual ICollection<tbl_Customer> tbl_CustomerIdentity { get; set; }
        public virtual ICollection<tbl_Customer> tbl_CustomerPassport { get; set; }

        public virtual ICollection<tbl_CustomerContact> tbl_CustomerContactIdentity { get; set; }
        public virtual ICollection<tbl_CustomerContact> tbl_CustomerContactPassport { get; set; }

        public virtual ICollection<tbl_Staff> tbl_StaffBirthplace { get; set; }
        public virtual ICollection<tbl_Staff> tbl_StaffIdentity { get; set; }
        public virtual ICollection<tbl_Staff> tbl_StaffPassport { get; set; }
        public virtual ICollection<tbl_Staff> tbl_StaffCitizenship { get; set; }

        public virtual ICollection<tbl_Partner> tbl_PartnerCountry { get; set; }
        public virtual ICollection<tbl_Partner> tbl_PartnerProvince { get; set; }
        public virtual ICollection<tbl_Partner> tbl_PartnerStyle { get; set; }

        public virtual ICollection<tbl_Tour> tbl_TourStartPlace { get; set; }
        public virtual ICollection<tbl_Tour> tbl_TourDestinationPlace { get; set; }

        public virtual ICollection<tbl_Quotation> tbl_QuotationCountry { get; set; }
        public virtual ICollection<tbl_TourGuide> tbl_TourGuide { get; set; }
        
        public virtual ICollection<tbl_Candidate> tbl_Candidate { get; set; }

        public virtual ICollection<tbl_Evaluation> tbl_EvaluationArea { get; set; }
        public virtual ICollection<tbl_Evaluation> tbl_EvaluationProvince { get; set; }
        public virtual ICollection<tbl_Airport> tbl_AirportProvince { get; set; }
        public virtual ICollection<tbl_Airport> tbl_AirportCountry { get; set; }

        public virtual ICollection<tbl_Ticket> tbl_TicketFirstDeparture { get; set; }
        public virtual ICollection<tbl_Ticket> tbl_TicketSecondDeparture { get; set; }
        public virtual ICollection<tbl_Ticket> tbl_TicketFirstDestination { get; set; }
        public virtual ICollection<tbl_Ticket> tbl_TicketSecondDestination { get; set; }
    }
}
