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
    public class tbl_Airport
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string Address { get; set; }
        [ForeignKey("tbl_TagsCountry")]
        public int? CountryId { get; set; }
        [ForeignKey("tbl_TagsProvince")]
        public int? ProvinceId { get; set; }
        [DefaultValue(false)]
        public Boolean IsDelete { get; set; }

        public virtual tbl_Tags tbl_TagsCountry { get; set; }
        public virtual tbl_Tags tbl_TagsProvince { get; set; }
        public virtual ICollection<tbl_Ticket> tbl_TicketFirstDeparture { get; set; }
        public virtual ICollection<tbl_Ticket> tbl_TicketSecondDeparture { get; set; }
        public virtual ICollection<tbl_Ticket> tbl_TicketFirstDestination { get; set; }
        public virtual ICollection<tbl_Ticket> tbl_TicketSecondDestination { get; set; }
    }
}
