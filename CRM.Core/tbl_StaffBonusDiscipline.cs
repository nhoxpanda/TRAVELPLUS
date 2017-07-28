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
    public class tbl_StaffBonusDiscipline
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("tbl_Staff")]
        public int StaffId { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Note { get; set; }
        public int TypeId { get; set; }
        [DefaultValue(false)]
        public Boolean IsDelete { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ActionDate { get; set; }

        public tbl_Staff tbl_Staff { get; set; }
    }
}
