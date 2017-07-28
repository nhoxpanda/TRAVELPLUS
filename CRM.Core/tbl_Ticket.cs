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
    public class tbl_Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Code { get; set; }
        public string CodeGroup { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Skyteam { get; set; }
        [ForeignKey("tbl_Customer")]
        public int? CustomerId { get; set; }
        [ForeignKey("tbl_DictionaryType")]
        public Nullable<int> TypeId { get; set; }
        public string TypeTicket { get; set; }
        [ForeignKey("tbl_DictionaryStatus")]
        public Nullable<int> StatusId { get; set; }
        [ForeignKey("tbl_Staff")]
        public int Staff { get; set; }
        public string Condition { get; set; }
        public decimal? Price { get; set; }
        [ForeignKey("tbl_DictionaryCurrency")]
        public Nullable<int> CurrencyId { get; set; }
        [DefaultValue(0)]
        public int Quantity { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [ForeignKey("tbl_Contract")]
        public int? ContractId { get; set; }
        [ForeignKey("tbl_Tour")]
        public int? TourId { get; set; }
        [ForeignKey("tbl_Program")]
        public int? ProgramId { get; set; }
        [ForeignKey("tbl_Airline")]
        public int? AirlineId { get; set; }
        [ForeignKey("tbl_DictionaryAirlineLeague")]
        public int? AirlineLeagueId { get; set; }
        [ForeignKey("tbl_DictionaryCardLevel")]
        public int? CardLevelId { get; set; }
        public decimal? DepositGroup { get; set; }
        public decimal? FarePrice { get; set; }
        public decimal? TaxPrice { get; set; }
        public decimal? ServicePrice { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? RosePrice { get; set; }
        public decimal? ComPrice { get; set; }
        public string Note { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        [DefaultValue(false)]
        public Boolean IsDelete { get; set; }
        ////////////
        [DefaultValue(false)]
        public Boolean IsRoundTrip { get; set; }
        [ForeignKey("tbl_TagsFirstDeparture")]
        public int? FirstDepartureId { get; set; }
        [ForeignKey("tbl_TagsFirstDestination")]
        public int? FirstDestinationId { get; set; }
        [ForeignKey("tbl_TagsSecondDeparture")]
        public int? SecondDepartureId { get; set; }
        [ForeignKey("tbl_TagsSecondDestination")]
        public int? SecondDestinationId { get; set; }
        [ForeignKey("tbl_AirportFirstDeparture")]
        public int? FirstDepartureAirportId { get; set; }
        [ForeignKey("tbl_AirportSecondDeparture")]
        public int? SecondDepartureAirportId { get; set; }
        [ForeignKey("tbl_AirportFirstDestination")]
        public int? FirstDestinationAirportId { get; set; }
        [ForeignKey("tbl_AirportSecondDestination")]
        public int? SecondDestinationAirportId { get; set; }

        public virtual tbl_Dictionary tbl_DictionaryType { get; set; }
        public virtual tbl_Dictionary tbl_DictionaryCardLevel { get; set; }
        public virtual tbl_Dictionary tbl_DictionaryAirlineLeague { get; set; }
        public virtual tbl_Dictionary tbl_DictionaryStatus { get; set; }
        public virtual tbl_Dictionary tbl_DictionaryCurrency { get; set; }
        public virtual tbl_Customer tbl_Customer { get; set; }
        public virtual tbl_Staff tbl_Staff { get; set; }
        public virtual tbl_Contract tbl_Contract { get; set; }
        public virtual tbl_Tour tbl_Tour { get; set; }
        public virtual tbl_Airline tbl_Airline { get; set; }
        public virtual tbl_Program tbl_Program { get; set; }

        public virtual tbl_Tags tbl_TagsFirstDeparture { get; set; }
        public virtual tbl_Tags tbl_TagsSecondDeparture { get; set; }
        public virtual tbl_Tags tbl_TagsFirstDestination { get; set; }
        public virtual tbl_Tags tbl_TagsSecondDestination { get; set; }
        public virtual tbl_Airport tbl_AirportFirstDeparture { get; set; }
        public virtual tbl_Airport tbl_AirportSecondDeparture { get; set; }
        public virtual tbl_Airport tbl_AirportFirstDestination { get; set; }
        public virtual tbl_Airport tbl_AirportSecondDestination { get; set; }
    }
}

