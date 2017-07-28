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
    public class tbl_VisaProcedure
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public string FileName { get; set; }
        [DefaultValue(false)]
        public Boolean IsDelete { get; set; }
        public virtual ICollection<tbl_VisaProcedureCustomer> tbl_VisaProcedureCustomer { get; set; }
    }
}
