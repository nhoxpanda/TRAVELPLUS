﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.Core;

namespace CRM.Infrastructure
{
    public class DataContext : DbContext
    //IdentityDbContext<ApplicationUser>
    {
        //new public virtual IDbSet<ApplicationRole> Roles { get; set; }
        public DataContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<DataContext>(null);
        }
        public static DataContext Create()
        {
            return new DataContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tbl_DictionaryCategory>();
            modelBuilder.Entity<tbl_Dictionary>();
            modelBuilder.Entity<tbl_Tags>();
            modelBuilder.Entity<tbl_Currency>();
            modelBuilder.Entity<tbl_DocumentFile>();
            modelBuilder.Entity<tbl_Customer>().HasOptional(p => p.tbl_Staff).WithMany(p => p.tbl_Customer).HasForeignKey(p => p.StaffId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Customer>().HasOptional(p => p.tbl_StaffManager).WithMany(p => p.tbl_ManageCustomer).HasForeignKey(p => p.StaffManager).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Customer>().HasOptional(p => p.tbl_DictionaryCareer).WithMany(p => p.tbl_CustomerCareer).HasForeignKey(p => p.CareerId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Customer>().HasOptional(p => p.tbl_DictionaryCustomerGroup).WithMany(p => p.tbl_CustomerCustomerGroup).HasForeignKey(p => p.CustomerGroupId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Customer>().HasOptional(p => p.tbl_DictionaryNameType).WithMany(p => p.tbl_CustomerNameType).HasForeignKey(p => p.NameTypeId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Customer>().HasOptional(p => p.tbl_DictionaryGender).WithMany(p => p.tbl_CustomerGender).HasForeignKey(p => p.GenderId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Customer>().HasOptional(p => p.tbl_DictionaryOrigin).WithMany(p => p.tbl_CustomerOrigin).HasForeignKey(p => p.OriginId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Customer>().HasOptional(p => p.tbl_TagsIdentity).WithMany(p => p.tbl_CustomerIdentity).HasForeignKey(p => p.IdentityTagId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Customer>().HasOptional(p => p.tbl_TagsPassport).WithMany(p => p.tbl_CustomerPassport).HasForeignKey(p => p.PassportTagId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_CustomerContact>().HasOptional(p => p.tbl_TagsIdentity).WithMany(p => p.tbl_CustomerContactIdentity).HasForeignKey(p => p.IdentityTagId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_CustomerContact>().HasOptional(p => p.tbl_TagsPassport).WithMany(p => p.tbl_CustomerContactPassport).HasForeignKey(p => p.PassportTagId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_CustomerVisa>().HasOptional(p => p.tbl_Dictionary).WithMany(p => p.tbl_CustomerVisa).HasForeignKey(p => p.DictionaryId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_CustomerVisa>().HasOptional(p => p.tbl_DictionaryType).WithMany(p => p.tbl_CustomerVisaType).HasForeignKey(p => p.VisaTypeId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_CustomerContactVisa>();
            modelBuilder.Entity<tbl_Form>();
            modelBuilder.Entity<tbl_Headquater>();
            modelBuilder.Entity<tbl_Module>();
            modelBuilder.Entity<tbl_StaffSalary>();
            modelBuilder.Entity<tbl_Staff>().HasOptional(p => p.tbl_DictionaryPosition).WithMany(p => p.tbl_StaffPosition).HasForeignKey(p => p.PositionId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Staff>().HasOptional(p => p.tbl_DictionaryDepartment).WithMany(p => p.tbl_StaffDepartment).HasForeignKey(p => p.DepartmentId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Staff>().HasOptional(p => p.tbl_DictionaryNational).WithMany(p => p.tbl_StaffNational).HasForeignKey(p => p.NationalId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Staff>().HasOptional(p => p.tbl_DictionaryReligion).WithMany(p => p.tbl_StaffReligion).HasForeignKey(p => p.ReligionId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Staff>().HasOptional(p => p.tbl_DictionaryCertificate).WithMany(p => p.tbl_StaffCertificate).HasForeignKey(p => p.CertificateId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Staff>().HasOptional(p => p.tbl_TagsBirthplace).WithMany(p => p.tbl_StaffBirthplace).HasForeignKey(p => p.Birthplace).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Staff>().HasOptional(p => p.tbl_TagsIdentity).WithMany(p => p.tbl_StaffIdentity).HasForeignKey(p => p.IdentityTagId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Staff>().HasOptional(p => p.tbl_TagsPassport).WithMany(p => p.tbl_StaffPassport).HasForeignKey(p => p.PassportTagId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Staff>().HasOptional(p => p.tbl_TagsCitizenship).WithMany(p => p.tbl_StaffCitizenship).HasForeignKey(p => p.CitizenshipId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Staff>().HasOptional(p => p.tbl_DictionaryNameType).WithMany(p => p.tbl_StaffNameType).HasForeignKey(p => p.NameTypeId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Staff>().HasOptional(p => p.tbl_DictionaryStaffGroup).WithMany(p => p.tbl_StaffGroup).HasForeignKey(p => p.StaffGroupId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Staff>().HasOptional(p => p.tbl_DictionaryContractType).WithMany(p => p.tbl_StaffContractType).HasForeignKey(p => p.ContractTypeId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_StaffVisa>().HasOptional(p => p.tbl_Dictionary).WithMany(p => p.tbl_StaffVisa).HasForeignKey(p => p.DictionaryId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_StaffVisa>().HasOptional(p => p.tbl_DictionaryType).WithMany(p => p.tbl_StaffVisaType).HasForeignKey(p => p.VisaType).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_StaffGroup>();
            modelBuilder.Entity<tbl_StaffDayOff>();
            modelBuilder.Entity<tbl_Partner>().HasOptional(p => p.tbl_TagsCountry).WithMany(p => p.tbl_PartnerCountry).HasForeignKey(p => p.CountryId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Partner>().HasOptional(p => p.tbl_TagsProvince).WithMany(p => p.tbl_PartnerProvince).HasForeignKey(p => p.ProvinceId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Partner>().HasOptional(p => p.tbl_TagsStyle).WithMany(p => p.tbl_PartnerStyle).HasForeignKey(p => p.StyleId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_PartnerNote>();
            modelBuilder.Entity<tbl_Function>();
            modelBuilder.Entity<tbl_Quotation>().HasOptional(p => p.tbl_TagsCountry).WithMany(p => p.tbl_QuotationCountry).HasForeignKey(p => p.CountryId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Quotation>().HasOptional(p => p.tbl_Staff).WithMany(p => p.tbl_QuotationStaff).HasForeignKey(p => p.StaffId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Quotation>().HasOptional(p => p.tbl_StaffQuotation).WithMany(p => p.tbl_QuotationStaffQ).HasForeignKey(p => p.StaffQuotationId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Quotation>().HasRequired(p => p.tbl_Dictionary).WithMany(p => p.tbl_Quotation).HasForeignKey(p => p.DictionaryId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Quotation>().HasOptional(p => p.tbl_DictionaryCurrency).WithMany(p => p.tbl_QuotationCurrency).HasForeignKey(p => p.CurrencyId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_QuotationForm>();
            modelBuilder.Entity<tbl_ReviewTour>();
            modelBuilder.Entity<tbl_ReviewTourDetail>();
            modelBuilder.Entity<tbl_Program>().HasOptional(p => p.tbl_Dictionary).WithMany(p => p.tbl_Program).HasForeignKey(p => p.DictionaryId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Program>().HasOptional(p => p.tbl_DictionaryCurrency).WithMany(p => p.tbl_ProgramCurrency).HasForeignKey(p => p.CurrencyId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Program>().HasOptional(p => p.tbl_DictionaryStatus).WithMany(p => p.tbl_ProgramStatus).HasForeignKey(p => p.StatusId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_AppointmentHistory>().HasOptional(p => p.tbl_Dictionary).WithMany(p => p.tbl_AppointmentHistory).HasForeignKey(p => p.DictionaryId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_AppointmentHistory>().HasOptional(p => p.tbl_DictionaryStatus).WithMany(p => p.tbl_AppointmentHistoryStatus).HasForeignKey(p => p.StatusId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_AppointmentHistory>().HasOptional(p => p.tbl_DictionaryService).WithMany(p => p.tbl_AppointmentHistoryService).HasForeignKey(p => p.ServiceId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_ContactHistory>().HasOptional(p => p.tbl_Staff).WithMany(p => p.tbl_ContactHistory).HasForeignKey(p => p.StaffId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_ContactHistory>().HasOptional(p => p.tbl_StaffOther).WithMany(p => p.tbl_ContactHistoryOther).HasForeignKey(p => p.OtherStaffId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_UpdateHistory>();
            modelBuilder.Entity<tbl_TourGuide>();
            modelBuilder.Entity<tbl_Tour>().HasOptional(p => p.tbl_TagsDestinationPlace).WithMany(p => p.tbl_TourDestinationPlace).HasForeignKey(p => p.DestinationPlace).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Tour>().HasOptional(p => p.tbl_TagsStartPlace).WithMany(p => p.tbl_TourStartPlace).HasForeignKey(p => p.StartPlace).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Tour>().HasOptional(p => p.tbl_DictionaryStatus).WithMany(p => p.tbl_TourStatus).HasForeignKey(p => p.StatusId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Tour>().HasOptional(p => p.tbl_DictionaryTypeTour).WithMany(p => p.tbl_TourTypeTour).HasForeignKey(p => p.TypeTourId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Tour>().HasOptional(p => p.tbl_Staff).WithMany(p => p.tbl_TourStaff).HasForeignKey(p => p.StaffId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Tour>().HasOptional(p => p.tbl_StaffCreate).WithMany(p => p.tbl_TourCreateStaff).HasForeignKey(p => p.CreateStaffId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_TourSchedule>().HasOptional(p => p.tbl_TourGuide).WithMany(p => p.tbl_TourScheduleGuide).HasForeignKey(p => p.TourGuideId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_TourSchedule>().HasOptional(p => p.tbl_Staff).WithMany(p => p.tbl_TourSchedule).HasForeignKey(p => p.StaffId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Contract>().HasOptional(p => p.tbl_Dictionary).WithMany(p => p.tbl_ContractDictionary).HasForeignKey(p => p.DictionaryId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Contract>().HasOptional(p => p.tbl_DictionaryStatus).WithMany(p => p.tbl_ContractStatus).HasForeignKey(p => p.StatusContractId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Contract>().HasOptional(p => p.tbl_DictionaryCurrency).WithMany(p => p.tbl_ContractCurrency).HasForeignKey(p => p.CurrencyId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Contract>().HasOptional(p => p.tbl_DictionaryCurrencyTDK).WithMany(p => p.tbl_ContractCurrencyTDK).HasForeignKey(p => p.CurrencyTDK).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Contract>().HasOptional(p => p.tbl_DictionaryCurrencyLNDK).WithMany(p => p.tbl_ContractCurrencyLNDK).HasForeignKey(p => p.CurrencyLNDK).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Promotion>();
            modelBuilder.Entity<tbl_Task>().HasOptional(p => p.tbl_DictionaryTaskPriority).WithMany(p => p.tbl_TaskPriority).HasForeignKey(p => p.TaskPriorityId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Task>().HasOptional(p => p.tbl_DictionaryTaskStatus).WithMany(p => p.tbl_TaskStatus).HasForeignKey(p => p.TaskStatusId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Task>().HasOptional(p => p.tbl_DictionaryTaskType).WithMany(p => p.tbl_TaskType).HasForeignKey(p => p.TaskTypeId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Task>().HasRequired(p => p.tbl_Staff).WithMany(p => p.tbl_Task).HasForeignKey(p => p.StaffId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_TaskNote>();
            modelBuilder.Entity<tbl_TaskHandling>();
            modelBuilder.Entity<tbl_TaskStaff>().HasRequired(p => p.tbl_Staff).WithMany(p => p.tbl_TaskStaff).HasForeignKey(p => p.StaffId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_TaskStaff>().HasRequired(p => p.tbl_StaffCreate).WithMany(p => p.tbl_TaskStaffCreate).HasForeignKey(p => p.CreateStaffId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_LiabilityCustomer>();
            modelBuilder.Entity<tbl_AccessData>();
            modelBuilder.Entity<tbl_ActionData>();
            modelBuilder.Entity<tbl_FormFunction>();
            modelBuilder.Entity<tbl_LiabilityPartner>().HasOptional(p => p.tbl_DictionaryCurrencyType1).WithMany(p => p.tbl_LiabilityPartner1).HasForeignKey(p => p.FirstCurrencyType).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_LiabilityPartner>().HasOptional(p => p.tbl_DictionaryCurrencyType2).WithMany(p => p.tbl_LiabilityPartner2).HasForeignKey(p => p.SecondCurrencyType).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_LiabilityPartner>().HasOptional(p => p.tbl_DictionaryService).WithMany(p => p.tbl_LiabilityPartnerService).HasForeignKey(p => p.ServiceId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_TourCustomer>();
            modelBuilder.Entity<tbl_TourCustomerVisa>();
            modelBuilder.Entity<tbl_ContractHandling>();
            modelBuilder.Entity<tbl_TourOption>().HasRequired(s => s.tbl_Dictionary).WithMany(s => s.tbl_TourOption).HasForeignKey(s => s.ServiceId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_TourOption>().HasRequired(s => s.tbl_Partner).WithMany(s => s.tbl_TourOption).HasForeignKey(s => s.PartnerId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_TourOption>().HasOptional(s => s.tbl_ServicesPartner).WithMany(s => s.tbl_TourOption).HasForeignKey(s => s.ServicePartnerId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_DeadlineOption>().HasRequired(s => s.tbl_ServicesPartner).WithMany(s => s.tbl_DeadlineOption).HasForeignKey(s => s.ServicesPartnerId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_DeadlineOption>().HasOptional(s => s.tbl_DictionaryCurrency).WithMany(s => s.tbl_DeadlineOptionCurrency).HasForeignKey(s => s.CurrencyId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_DeadlineOption>().HasOptional(s => s.tbl_Dictionary).WithMany(s => s.tbl_DeadlineOption).HasForeignKey(s => s.StatusId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_QuyTacDanhSo>();
            modelBuilder.Entity<tbl_ShowDataBy>();
            modelBuilder.Entity<tbl_Permissions>();
            modelBuilder.Entity<tbl_VisaInfomation>().HasOptional(s => s.tbl_DictionaryCurrency).WithMany(s => s.tbl_VisaInfomation).HasForeignKey(s => s.CurrencyId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_VisaInfomation>().HasOptional(s => s.tbl_DictionaryCurrencyService).WithMany(s => s.tbl_VisaInfomationService).HasForeignKey(s => s.CurrencyServiceId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_VisaInfomation>().HasOptional(s => s.tbl_DictionaryCategoryVisa).WithMany(s => s.tbl_VisaInfomationCategory).HasForeignKey(s => s.CategoryVisaId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_ServicesPartner>();
            modelBuilder.Entity<tbl_ContractReceipt>();
            modelBuilder.Entity<tbl_Conversation>();
            modelBuilder.Entity<tbl_ConversationReply>();
            modelBuilder.Entity<tbl_GroupChat>();
            modelBuilder.Entity<tbl_Message>();
            modelBuilder.Entity<tbl_Logo>();
            modelBuilder.Entity<tbl_InvoicePartner>();
            modelBuilder.Entity<tbl_MailCategory>();
            modelBuilder.Entity<tbl_MailConfig>();
            modelBuilder.Entity<tbl_MailFields>();
            modelBuilder.Entity<tbl_MailReceiveList>();
            modelBuilder.Entity<tbl_MailReceives>();
            modelBuilder.Entity<tbl_MailSending>();
            modelBuilder.Entity<tbl_MailSendingList>();
            modelBuilder.Entity<tbl_MailSendingReceives>();
            modelBuilder.Entity<tbl_MailTemplates>();
            modelBuilder.Entity<tbl_MailImport>();
            modelBuilder.Entity<tbl_StaffBonusDiscipline>();
            modelBuilder.Entity<tbl_Ticket>().HasOptional(s => s.tbl_DictionaryCardLevel).WithMany(s => s.tbl_TicketCardLevel).HasForeignKey(s => s.CardLevelId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Ticket>().HasOptional(s => s.tbl_DictionaryAirlineLeague).WithMany(s => s.tbl_TicketAirlineLeague).HasForeignKey(s => s.AirlineLeagueId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Ticket>().HasOptional(s => s.tbl_DictionaryCurrency).WithMany(s => s.tbl_TicketCurrency).HasForeignKey(s => s.CurrencyId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Ticket>().HasOptional(s => s.tbl_DictionaryStatus).WithMany(s => s.tbl_TicketStatus).HasForeignKey(s => s.StatusId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Ticket>().HasOptional(s => s.tbl_DictionaryType).WithMany(s => s.tbl_TicketType).HasForeignKey(s => s.TypeId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Ticket>().HasOptional(s => s.tbl_TagsFirstDeparture).WithMany(s => s.tbl_TicketFirstDeparture).HasForeignKey(s => s.FirstDepartureId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Ticket>().HasOptional(s => s.tbl_TagsSecondDeparture).WithMany(s => s.tbl_TicketSecondDeparture).HasForeignKey(s => s.SecondDepartureId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Ticket>().HasOptional(s => s.tbl_TagsFirstDestination).WithMany(s => s.tbl_TicketFirstDestination).HasForeignKey(s => s.FirstDestinationId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Ticket>().HasOptional(s => s.tbl_TagsSecondDestination).WithMany(s => s.tbl_TicketSecondDestination).HasForeignKey(s => s.SecondDestinationId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Ticket>().HasOptional(s => s.tbl_AirportFirstDeparture).WithMany(s => s.tbl_TicketFirstDeparture).HasForeignKey(s => s.FirstDepartureAirportId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Ticket>().HasOptional(s => s.tbl_AirportSecondDeparture).WithMany(s => s.tbl_TicketSecondDeparture).HasForeignKey(s => s.SecondDepartureAirportId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Ticket>().HasOptional(s => s.tbl_AirportFirstDestination).WithMany(s => s.tbl_TicketFirstDestination).HasForeignKey(s => s.FirstDestinationAirportId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Ticket>().HasOptional(s => s.tbl_AirportSecondDestination).WithMany(s => s.tbl_TicketSecondDestination).HasForeignKey(s => s.SecondDestinationAirportId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Candidate>().HasOptional(s => s.tbl_DictionaryNameType).WithMany(s => s.tbl_CandidateNameType).HasForeignKey(s => s.NameTypeId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Candidate>().HasOptional(s => s.tbl_DictionaryNational).WithMany(s => s.tbl_CandidateNational).HasForeignKey(s => s.NationalId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Candidate>().HasOptional(s => s.tbl_DictionaryReligionId).WithMany(s => s.tbl_CandidateRelegion).HasForeignKey(s => s.ReligionId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_MemberCard>();
            modelBuilder.Entity<tbl_MemberCardHistory>();
            modelBuilder.Entity<tbl_EvaluationDetail>();
            modelBuilder.Entity<tbl_EvaluationCriteria>();
            modelBuilder.Entity<tbl_Evaluation>().HasOptional(s => s.tbl_TagsArea).WithMany(s => s.tbl_EvaluationArea).HasForeignKey(s => s.AreaId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Evaluation>().HasOptional(s => s.tbl_TagsProvince).WithMany(s => s.tbl_EvaluationProvince).HasForeignKey(s => s.ProvinceId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Evaluation>().HasRequired(p => p.tbl_Staff).WithMany(p => p.tbl_Evaluation).HasForeignKey(p => p.StaffId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Evaluation>().HasRequired(p => p.tbl_StaffCreate).WithMany(p => p.tbl_EvaluationCreate).HasForeignKey(p => p.CreateStaffId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Airline>();
            modelBuilder.Entity<tbl_AirlineTicket>();
            modelBuilder.Entity<tbl_TourNote>();
            modelBuilder.Entity<tbl_StaffSalaryDetail>();
            modelBuilder.Entity<tbl_Bank>();
            modelBuilder.Entity<tbl_BankDetail>();
            modelBuilder.Entity<tbl_VisaProcedure>();
            modelBuilder.Entity<tbl_VisaProcedureCustomer>();
            modelBuilder.Entity<tbl_ReceiptBill>().HasOptional(s => s.tbl_DictionaryCurrency).WithMany(s => s.tbl_ReceiptBillCurrency).HasForeignKey(s => s.CurrencyId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_ReceiptBill>().HasOptional(s => s.tbl_DictionaryStatus).WithMany(s => s.tbl_ReceiptBillStatus).HasForeignKey(s => s.StatusId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_ReceiptBill>().HasOptional(s => s.tbl_Staff).WithMany(s => s.tbl_ReceiptBill).HasForeignKey(s => s.StaffId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_ReceiptBill>().HasOptional(s => s.tbl_StaffReceive).WithMany(s => s.tbl_ReceiptBillReceipt).HasForeignKey(s => s.StaffReceiveId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_PaymentBill>().HasOptional(s => s.tbl_DictionaryCurrency).WithMany(s => s.tbl_PaymentBillCurrency).HasForeignKey(s => s.CurrencyId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_PaymentBill>().HasOptional(s => s.tbl_DictionaryStatus).WithMany(s => s.tbl_PaymentBillStatus).HasForeignKey(s => s.StatusId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_PaymentBillPeriod>();
            modelBuilder.Entity<tbl_ReceiptBillPeriod>();
            modelBuilder.Entity<tbl_TourCalculation>();
            modelBuilder.Entity<tbl_POApproval>().HasOptional(s => s.tbl_DictionaryCurrency).WithMany(s => s.tbl_POApprovalCurrency).HasForeignKey(s => s.CurrencyId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_POApproval>().HasOptional(s => s.tbl_DictionaryPaymentMethod).WithMany(s => s.tbl_POApprovalPaymentMethod).HasForeignKey(s => s.PaymentMethodId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_POApproval>().HasOptional(s => s.tbl_DictionaryStatus).WithMany(s => s.tbl_POApprovalStatus).HasForeignKey(s => s.StatusId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_POApprovalDetail>();
            modelBuilder.Entity<tbl_POApprovalHistory>();
            modelBuilder.Entity<tbl_POApprovalPermission>();
            modelBuilder.Entity<tbl_Airport>().HasOptional(s => s.tbl_TagsProvince).WithMany(s => s.tbl_AirportProvince).HasForeignKey(s => s.ProvinceId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_Airport>().HasOptional(s => s.tbl_TagsCountry).WithMany(s => s.tbl_AirportCountry).HasForeignKey(s => s.CountryId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_CustomerLeague>().HasOptional(s => s.tbl_DictionaryAirlineLeague).WithMany(s => s.tbl_CustomerLeagueAirline).HasForeignKey(s => s.AirlineLeagueId).WillCascadeOnDelete(false);
            modelBuilder.Entity<tbl_CustomerLeague>().HasOptional(s => s.tbl_DictionaryCardLevel).WithMany(s => s.tbl_CustomerLeagueCardLevel).HasForeignKey(s => s.CardLevelId).WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<tbl_DictionaryCategory> tbl_DictionaryCategory { get; set; }
        public DbSet<tbl_Dictionary> tbl_Dictionary { get; set; }
        public DbSet<tbl_Tags> tbl_Tags { get; set; }
        public DbSet<tbl_DocumentFile> tbl_DocumentFile { get; set; }
        public DbSet<tbl_Customer> tbl_Customer { get; set; }
        public DbSet<tbl_CustomerVisa> tbl_CustomerVisa { get; set; }
        public DbSet<tbl_Form> tbl_Form { get; set; }
        public DbSet<tbl_Headquater> tbl_Headquater { get; set; }
        public DbSet<tbl_Module> tbl_Module { get; set; }
        public DbSet<tbl_Staff> tbl_Staff { get; set; }
        public DbSet<tbl_StaffVisa> tbl_StaffVisa { get; set; }
        public DbSet<tbl_StaffGroup> tbl_StaffGroup { get; set; }
        public DbSet<tbl_Partner> tbl_Partner { get; set; }
        public DbSet<tbl_PartnerNote> tbl_PartnerNote { get; set; }
        public DbSet<tbl_Quotation> tbl_Quotation { get; set; }
        public DbSet<tbl_QuotationForm> tbl_QuotationForm { get; set; }
        public DbSet<tbl_Tour> tbl_Tour { get; set; }
        public DbSet<tbl_TourGuide> tbl_TourGuide { get; set; }
        public DbSet<tbl_ServicesPartner> tbl_ServicesPartner { get; set; }
        public DbSet<tbl_Function> tbl_Function { get; set; }
        public DbSet<tbl_CustomerContactVisa> tbl_CustomerContactVisa { get; set; }
        public DbSet<tbl_CustomerContact> tbl_CustomerContact { get; set; }
        public DbSet<tbl_ReviewTourDetail> tbl_ReviewTourDetail { get; set; }
        public DbSet<tbl_ReviewTour> tbl_ReviewTour { get; set; }
        public DbSet<tbl_Program> tbl_Program { get; set; }
        public DbSet<tbl_Contract> tbl_Contract { get; set; }
        public DbSet<tbl_Promotion> tbl_Promotion { get; set; }
        public DbSet<tbl_AppointmentHistory> tbl_AppointmentHistory { get; set; }
        public DbSet<tbl_ContactHistory> tbl_ContactHistory { get; set; }
        public DbSet<tbl_UpdateHistory> tbl_UpdateHistory { get; set; }
        public DbSet<tbl_Task> tbl_Task { get; set; }
        public DbSet<tbl_TaskNote> tbl_TaskNote { get; set; }
        public DbSet<tbl_TaskHandling> tbl_TaskHandling { get; set; }
        public DbSet<tbl_TaskStaff> tbl_TaskStaff { get; set; }
        public DbSet<tbl_LiabilityCustomer> tbl_LiabilityCustomer { get; set; }
        public DbSet<tbl_LiabilityPartner> tbl_LiabilityPartner { get; set; }
        public DbSet<tbl_ActionData> tbl_ActionData { get; set; }
        public DbSet<tbl_AccessData> tbl_AccessData { get; set; }
        public DbSet<tbl_FormFunction> tbl_FormFunction { get; set; }
        public DbSet<tbl_TourSchedule> tbl_TourSchedule { get; set; }
        public DbSet<tbl_TourCustomer> tbl_TourCustomer { get; set; }
        public DbSet<tbl_TourCustomerVisa> tbl_TourCustomerVisa { get; set; }
        public DbSet<tbl_TourOption> tbl_TourOption { get; set; }
        public DbSet<tbl_DeadlineOption> tbl_DeadlineOption { get; set; }
        public DbSet<tbl_ContractHandling> tbl_ContractHandling { get; set; }
        public DbSet<tbl_ContractReceipt> tbl_ContractReceipt { get; set; }
        public DbSet<tbl_QuyTacDanhSo> tbl_QuyTacDanhSo { get; set; }
        public DbSet<tbl_ShowDataBy> tbl_ShowDataBy { get; set; }
        public DbSet<tbl_Permissions> tbl_Permissions { get; set; }
        public DbSet<tbl_Conversation> tbl_Conversation { get; set; }
        public DbSet<tbl_ConversationReply> tbl_ConversationReply { get; set; }
        public DbSet<tbl_GroupChat> tbl_GroupChat { get; set; }
        public DbSet<tbl_Message> tbl_Message { get; set; }
        public DbSet<tbl_Currency> tbl_Currency { get; set; }
        public DbSet<tbl_Logo> tbl_Logo { get; set; }
        public DbSet<tbl_MailCategory> tbl_MailCategory { get; set; }
        public DbSet<tbl_MailConfig> tbl_MailConfig { get; set; }
        public DbSet<tbl_MailFields> tbl_MailFields { get; set; }
        public DbSet<tbl_MailReceiveList> tbl_MailReceiveList { get; set; }
        public DbSet<tbl_MailReceives> tbl_MailReceives { get; set; }
        public DbSet<tbl_MailSending> tbl_MailSending { get; set; }
        public DbSet<tbl_MailSendingList> tbl_MailSendingList { get; set; }
        public DbSet<tbl_MailSendingReceives> tbl_MailSendingReceives { get; set; }
        public DbSet<tbl_MailTemplates> tbl_MailTemplates { get; set; }
        public DbSet<tbl_MailImport> tbl_MailImport { get; set; }
        public DbSet<tbl_Ticket> tbl_Ticket { get; set; }
        public DbSet<tbl_StaffSalary> tbl_StaffSalary { get; set; }
        public DbSet<tbl_StaffBonusDiscipline> tbl_StaffBonusDiscipline { get; set; }
        public DbSet<tbl_StaffDayOff> tbl_StaffDayOff { get; set; }
        public DbSet<tbl_Candidate> tbl_Candidate { get; set; }
        public DbSet<tbl_MemberCard> tbl_MemberCard { get; set; }
        public DbSet<tbl_MemberCardHistory> tbl_MemberCardHistory { get; set; }
        public DbSet<tbl_Evaluation> tbl_Evaluation { get; set; }
        public DbSet<tbl_EvaluationCriteria> tbl_EvaluationCriteria { get; set; }
        public DbSet<tbl_EvaluationDetail> tbl_EvaluationDetail { get; set; }
        public DbSet<tbl_Airline> tbl_Airline { get; set; }
        public DbSet<tbl_AirlineTicket> tbl_AirlineTicket { get; set; }
        public DbSet<tbl_TourNote> tbl_TourNote { get; set; }
        public DbSet<tbl_StaffSalaryDetail> tbl_StaffSalaryDetail { get; set; }
        public DbSet<tbl_Bank> tbl_Bank { get; set; }
        public DbSet<tbl_BankDetail> tbl_BankDetail { get; set; }
        public DbSet<tbl_VisaProcedure> tbl_VisaProcedure { get; set; }
        public DbSet<tbl_VisaProcedureCustomer> tbl_VisaProcedureCustomer { get; set; }
        public DbSet<tbl_ReceiptBill> tbl_ReceiptBill { get; set; }
        public DbSet<tbl_ReceiptBillPeriod> tbl_ReceiptBillPeriod { get; set; }
        public DbSet<tbl_PaymentBill> tbl_PaymentBill { get; set; }
        public DbSet<tbl_PaymentBillPeriod> tbl_PaymentBillPeriod { get; set; }
        public DbSet<tbl_TourCalculation> tbl_TourCalculation { get; set; }
        public DbSet<tbl_POApproval> tbl_POApproval { get; set; }
        public DbSet<tbl_POApprovalDetail> tbl_POApprovalDetail { get; set; }
        public DbSet<tbl_POApprovalHistory> tbl_POApprovalHistory { get; set; }
        public DbSet<tbl_POApprovalPermission> tbl_POApprovalPermission { get; set; }
        public DbSet<tbl_Airport> tbl_Airport { get; set; }
        public DbSet<tbl_CustomerLeague> tbl_CustomerLeague { get; set; }
    }
}