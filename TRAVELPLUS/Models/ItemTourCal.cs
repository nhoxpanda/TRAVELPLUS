using CRM.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRAVELPLUS.Models
{
    public class ItemTourCal
    {
        public ItemTourCal()
        {
            listServices = new List<ServicesPrice>();
        }
        public string DateItem { get; set; }
        public IList<ServicesPrice> listServices { get; set; }
    }
    public class ServicesPrice
    {
        public ServicesPrice()
        {
            Service = new tbl_Dictionary();
        }
        public int Price { get; set; }
        public tbl_Dictionary Service { get; set; }
    }
}