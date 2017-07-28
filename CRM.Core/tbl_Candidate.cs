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
    public class tbl_Candidate
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        public Nullable<int> NameTypeId { get; set; }
        public string FullName { get; set; }
        public Boolean Gender { get; set; }
        public Nullable<DateTime> Birthday { get; set; }
        public Nullable<int> Birthplace { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Nullable<int> HeadquarterId { get; set; }
        public string IdentityCard { get; set; }
        public Nullable<DateTime> CreatedDateIdentity { get; set; }
        public Nullable<int> IdentityTagId { get; set; }
        public Nullable<int> NationalId { get; set; }
        public Nullable<int> ReligionId { get; set; }
        public string HouseholdAdress { get; set; }
        public string Technique { get; set; }
        public string Language { get; set; }
        public string InformationTechnology { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public string Skype { get; set; }
        [DefaultValue(false)]
        public Boolean IsDelete { get; set; }
        public Nullable<int> StaffId { get; set; }
        public string Note { get; set; }
        public string Image { get; set; }
        public Nullable<DateTime> ApplyDate { get; set; }

        [ForeignKey("HeadquarterId")]
        public virtual tbl_Headquater tbl_Headquater { get; set; }
        [ForeignKey("NameTypeId")]
        public virtual tbl_Dictionary tbl_DictionaryNameType { get; set; }
        [ForeignKey("NationalId")]
        public virtual tbl_Dictionary tbl_DictionaryNational { get; set; }
        [ForeignKey("ReligionId")]
        public virtual tbl_Dictionary tbl_DictionaryReligionId { get; set; }
        [ForeignKey("IdentityTagId")]
        public virtual tbl_Tags tbl_Tags { get; set; }
        [ForeignKey("StaffId")]
        public virtual tbl_Staff tbl_Staff { get; set; }
    }
}