using CRM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TRAVELPLUS.Models
{
    public class TourCalModel
    {
        public TourCalModel()
        {
            listDate = new List<SelectListItem>();
            listTourCal = new List<tbl_TourCalculation>();
        }
        public IList<tbl_TourCalculation> listTourCal { get; set; }
        public IList<SelectListItem> listDate { get; set; }
        public IList<SelectListItem> listService { get; set; }
        public IList<SelectListItem> listPartner { get; set; }
        public string DateSelect { get; set; }
        public string TourName { get; set; }
        public int ServiceID { get; set; }
        public int TourID { get; set; }
        public int PartnerID { get; set; }
        public int CurrencyId { get; set; }
        public decimal? PriceTourCal { get; set; }
        public string Note { get; set; }
        public int tongTien { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int SoNgay { get; set; }
    }
}