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
    public class tbl_Airline
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Logo { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string ContactName { get; set; }
        public decimal? Percent { get; set; }
        public string Note { get; set; }
        public DateTime CreateDate { get; set; }
        [DefaultValue(false)]
        public Boolean IsDelete { get; set; }
        public Nullable<int> StaffId { get; set; }

        [ForeignKey("StaffId")]
        public virtual tbl_Staff tbl_Staff { get; set; }
        public virtual ICollection<tbl_AirlineTicket> tbl_AirlineTicket { get; set; }
        public virtual ICollection<tbl_Ticket> tbl_Ticket { get; set; }
    }
}