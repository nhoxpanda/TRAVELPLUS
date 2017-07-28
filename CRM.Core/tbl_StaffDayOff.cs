using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core
{
    public class tbl_StaffDayOff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("tbl_Staff")]
        public int StaffId { get; set; }
        public decimal? NumberDay { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDate { get; set; }
        public string Note { get; set; }

        public tbl_Staff tbl_Staff { get; set; }
    }
}
