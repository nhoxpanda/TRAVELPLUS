using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Enum;
using System.ComponentModel;

namespace CRM.Core
{
    public class tbl_Staff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        public string TaxCode { get; set; }
        public Nullable<int> NameTypeId { get; set; }
        public string FullName { get; set; }
        public Boolean Gender { get; set; }
        public Nullable<DateTime> Birthday { get; set; }
        public Nullable<int> Birthplace { get; set; }
        public string Address { get; set; }
        public string TagsId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsLock { get; set; }
        public Nullable<int> PositionId { get; set; }
        public Nullable<int> StaffGroupId { get; set; }
        [ForeignKey("tbl_Headquater")]
        public Nullable<int> HeadquarterId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public string IdentityCard { get; set; }
        public Nullable<DateTime> CreatedDateIdentity { get; set; }
        public Nullable<int> IdentityTagId { get; set; }
        public Nullable<int> NationalId { get; set; }
        public Nullable<int> ReligionId { get; set; }
        public string HouseholdAdress { get; set; }
        public Nullable<Marriage> Marriage { get; set; }
        public Nullable<int> InternalNumber { get; set; }
        public Nullable<int> CertificateId { get; set; }
        public string Technique { get; set; }
        public string Language { get; set; }
        public string InformationTechnology { get; set; }
        public string AccountNumber { get; set; }
        public string Bank { get; set; }
        public string PassportCard { get; set; }
        public Nullable<DateTime> CreatedDatePassport { get; set; }
        public Nullable<DateTime> ExpiredDatePassport { get; set; }
        public Nullable<int> PassportTagId { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public Nullable<int> RoleGroupId { get; set; }
        public string Skype { get; set; }
        [DefaultValue(false)]
        public Boolean IsDelete { get; set; }
        public Nullable<int> PermissionId { get; set; }
        public Nullable<int> StaffId { get; set; }
        public string Note { get; set; }
        public string Image { get; set; }
        public string CodeGuide { get; set; }
        [DefaultValue(false)]
        public Boolean IsGuide { get; set; }
        [DefaultValue(true)]
        public Boolean IsVietlike { get; set; }
        public string Password { get; set; }
        public Nullable<DateTime> StartWork { get; set; }
        public Nullable<DateTime> EndWork { get; set; }
        public Nullable<DateTime> ContractDate { get; set; }
        public string ContractType { get; set; }
        public Nullable<int>  ContractTypeId { get; set; }
        [DefaultValue(0)]
        public int NumberYear { get; set; }
        [DefaultValue(0)]
        public decimal? NumberDayOff { get; set; }
        public decimal? NumberDayOffOld { get; set; }
        public decimal? NumdayOffLeft { get; set; }
        public Guid TokenKey { get; set; }
        public Nullable<int> CitizenshipId { get; set; }
        public Nullable<int> ExperienceYear { get; set; }
        public string ExperienceNote { get; set; }
        public string ExperienceTP { get; set; }
        public string Seniority { get; set; }
        public Nullable<DateTime> ContractFromDate { get; set; }
        public Nullable<DateTime> ContractEndDate { get; set; }

        public virtual ICollection<tbl_POApproval> tbl_POApproval { get; set; }
        public virtual ICollection<tbl_POApprovalHistory> tbl_POApprovalHistory { get; set; }
        public virtual ICollection<tbl_StaffBonusDiscipline> tbl_StaffBonusDiscipline { get; set; }
        public virtual ICollection<tbl_VisaProcedureCustomer> tbl_VisaProcedureCustomer { get; set; }
        public virtual ICollection<tbl_BankDetail> tbl_BankDetail { get; set; }
        public virtual ICollection<tbl_StaffDayOff> tbl_StaffDayOff { get; set; }
        public virtual ICollection<tbl_StaffSalary> tbl_StaffSalary { get; set; }
        public virtual ICollection<tbl_Ticket> tbl_Ticket { get; set; }
        public virtual ICollection<tbl_DeadlineOption> tbl_DeadlineOption { get; set; }
        public virtual ICollection<tbl_Task> tbl_Task { get; set; }
        public virtual ICollection<tbl_Tour> tbl_TourStaff { get; set; }
        public virtual ICollection<tbl_Tour> tbl_TourCreateStaff { get; set; }
        public virtual ICollection<tbl_TourSchedule> tbl_TourSchedule { get; set; }
        public virtual ICollection<tbl_TourSchedule> tbl_TourScheduleGuide { get; set; }
        public virtual ICollection<tbl_Quotation> tbl_QuotationStaff { get; set; }
        public virtual ICollection<tbl_Quotation> tbl_QuotationStaffQ { get; set; }
        public virtual ICollection<tbl_QuotationForm> tbl_QuotationForm { get; set; }
        public virtual ICollection<tbl_DocumentFile> tbl_DocumentFile { get; set; }
        public virtual ICollection<tbl_StaffVisa> tbl_StaffVisa { get; set; }
        public virtual ICollection<tbl_Headquater> tbl_Headquater { get; set; }
        public virtual ICollection<tbl_ReviewTour> tbl_ReviewTour { get; set; }
        public virtual ICollection<tbl_Program> tbl_Program { get; set; }
        public virtual ICollection<tbl_ContactHistory> tbl_ContactHistory { get; set; }
        public virtual ICollection<tbl_ContactHistory> tbl_ContactHistoryOther { get; set; }
        public virtual ICollection<tbl_UpdateHistory> tbl_UpdateHistory { get; set; }
        public virtual ICollection<tbl_AppointmentHistory> tbl_AppointmentHistory { get; set; }
        public virtual ICollection<tbl_Promotion> tbl_Promotion { get; set; }
        public virtual ICollection<tbl_TaskHandling> tbl_TaskHandling { get; set; }
        public virtual ICollection<tbl_TaskStaff> tbl_TaskStaff { get; set; }
        public virtual ICollection<tbl_TaskStaff> tbl_TaskStaffCreate { get; set; }
        public virtual ICollection<tbl_TaskNote> tbl_TaskNote { get; set; }
        public virtual ICollection<tbl_TourGuide> tbl_TourGuide { get; set; }
        public virtual ICollection<tbl_LiabilityCustomer> tbl_LiabilityCustomer { get; set; }
        public virtual ICollection<tbl_LiabilityPartner> tbl_LiabilityPartner { get; set; }
        public virtual ICollection<tbl_Partner> tbl_Partner { get; set; }
        public virtual ICollection<tbl_ContractReceipt> tbl_ContractReceipt { get; set; }
        public virtual ICollection<tbl_Customer> tbl_Customer { get; set; }
        public virtual ICollection<tbl_Customer> tbl_ManageCustomer { get; set; }
        public virtual ICollection<tbl_TourCalculation> tbl_TourCalculation { get; set; }
        public virtual ICollection<tbl_InvoicePartner> tbl_InvoicePartner { get; set; }
        public virtual ICollection<tbl_MailConfig> tbl_MailConfig { get; set; }
        public virtual ICollection<tbl_MailReceives> tbl_MailReceives { get; set; }
        public virtual ICollection<tbl_MailReceiveList> tbl_MailReceiveList { get; set; }
        public virtual ICollection<tbl_MailSending> tbl_MailSending { get; set; }
        public virtual ICollection<tbl_MailTemplates> tbl_MailTemplates { get; set; }
        public virtual ICollection<tbl_MailImport> tbl_MailImport { get; set; }
        public virtual ICollection<tbl_Candidate> tbl_Candidate { get; set; }
        public virtual ICollection<tbl_MemberCardHistory> tbl_MemberCardHistory { get; set; }
        public virtual ICollection<tbl_Evaluation> tbl_Evaluation { get; set; }
        public virtual ICollection<tbl_Evaluation> tbl_EvaluationCreate { get; set; }
        public virtual ICollection<tbl_Airline> tbl_Airline { get; set; }
        public virtual ICollection<tbl_AirlineTicket> tbl_AirlineTicket { get; set; }
        public virtual ICollection<tbl_ReceiptBill> tbl_ReceiptBill { get; set; }
        public virtual ICollection<tbl_ReceiptBill> tbl_ReceiptBillReceipt { get; set; }
        public virtual ICollection<tbl_PaymentBill> tbl_PaymentBill { get; set; }

        [ForeignKey("ContractTypeId")]
        public virtual tbl_Dictionary tbl_DictionaryContractType { get; set; }
        [ForeignKey("StaffGroupId")]
        public virtual tbl_Dictionary tbl_DictionaryStaffGroup { get; set; }
        [ForeignKey("NameTypeId")]
        public virtual tbl_Dictionary tbl_DictionaryNameType { get; set; }
        [ForeignKey("PositionId")]
        public virtual tbl_Dictionary tbl_DictionaryPosition { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual tbl_Dictionary tbl_DictionaryDepartment { get; set; }
        [ForeignKey("NationalId")]
        public virtual tbl_Dictionary tbl_DictionaryNational { get; set; }
        [ForeignKey("ReligionId")]
        public virtual tbl_Dictionary tbl_DictionaryReligion { get; set; }
        [ForeignKey("CertificateId")]
        public virtual tbl_Dictionary tbl_DictionaryCertificate { get; set; }
        [ForeignKey("PermissionId")]
        public virtual tbl_Permissions tbl_Permissions { get; set; }
        [ForeignKey("Birthplace")]
        public virtual tbl_Tags tbl_TagsBirthplace { get; set; }
        [ForeignKey("IdentityTagId")]
        public virtual tbl_Tags tbl_TagsIdentity { get; set; }
        [ForeignKey("PassportTagId")]
        public virtual tbl_Tags tbl_TagsPassport { get; set; }
        [ForeignKey("CitizenshipId")]
        public virtual tbl_Tags tbl_TagsCitizenship { get; set; }
    }
}
