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
    public class tbl_AirlineTicket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string Note { get; set; }
        public Nullable<int> AirlineId { get; set; }
        public DateTime CreateDate { get; set; }
        [DefaultValue(false)]
        public Boolean IsDelete { get; set; }
        public Nullable<int> StaffId { get; set; }

        [ForeignKey("AirlineId")]
        public virtual tbl_Airline tbl_Airline { get; set; }
        [ForeignKey("StaffId")]
        public virtual tbl_Staff tbl_Staff { get; set; }
    }
}
