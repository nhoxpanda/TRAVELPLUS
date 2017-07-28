using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core
{
    public class tbl_TourNote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Note { get; set; }
        public int TourId { get; set; }
        public DateTime CreatedDate { get; set; }

        [ForeignKey("TourId")]
        public virtual tbl_Tour tbl_Tour { get; set; }
    }
}
