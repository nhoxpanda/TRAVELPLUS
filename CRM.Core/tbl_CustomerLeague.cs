using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Core
{
    public class tbl_CustomerLeague
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? AirlineLeagueId { get; set; }
        public int? CardLevelId { get; set; }
        public string Code { get; set; }
        public int? CustomerId { get; set; }

        [ForeignKey("AirlineLeagueId")]
        public virtual tbl_Dictionary tbl_DictionaryAirlineLeague { get; set; }
        [ForeignKey("CardLevelId")]
        public virtual tbl_Dictionary tbl_DictionaryCardLevel { get; set; }
        [ForeignKey("CustomerId")]
        public virtual tbl_Customer tbl_Customer { get; set; }
    }
}
