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
    public class tbl_Evaluation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Nullable<int> PartnerId { get; set; }
        public Nullable<int> StaffId { get; set; }
        public Nullable<int> AreaId { get; set; }
        public Nullable<int> ProvinceId { get; set; }
        public Nullable<int> TourId { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public decimal? Point { get; set; }
        public Nullable<int> CreateStaffId { get; set; }
        public DateTime CreatedDate { get; set; }
        [DefaultValue(false)]
        public Boolean IsDelete { get; set; }
        public int Type { get; set; }

        [ForeignKey("TourId")]
        public tbl_Tour tbl_Tour { get; set; }
        [ForeignKey("PartnerId")]
        public tbl_Partner tbl_Partner { get; set; }
        [ForeignKey("StaffId")]
        public tbl_Staff tbl_Staff { get; set; }
        [ForeignKey("CreateStaffId")]
        public tbl_Staff tbl_StaffCreate { get; set; }
        [ForeignKey("AreaId")]
        public tbl_Tags tbl_TagsArea { get; set; }
        [ForeignKey("ProvinceId")]
        public tbl_Tags tbl_TagsProvince { get; set; }
        public virtual ICollection<tbl_EvaluationDetail> tbl_EvaluationDetail { get; set; }
    }
}
