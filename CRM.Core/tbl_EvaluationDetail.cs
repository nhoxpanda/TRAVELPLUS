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
    public class tbl_EvaluationDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Nullable<int> EvaluationId { get; set; }
        public Nullable<int> EvaluationCriteriaId { get; set; }
        public int Point { get; set; }
        public string Note { get; set; }

        [ForeignKey("EvaluationCriteriaId")]
        public tbl_EvaluationCriteria tbl_EvaluationCriteria { get; set; }
        [ForeignKey("EvaluationId")]
        public tbl_Evaluation tbl_Evaluation { get; set; }
    }
}
