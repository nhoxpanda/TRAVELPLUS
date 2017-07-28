using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TRAVELPLUS.Models
{
    public class TicketViewModel
    {
        public decimal? Percent { get; set; }
        public int? DepartureId1 { get; set; }
        public int? Destination1 { get; set; }
        public int? DepartureId2 { get; set; }
        public int? Destination2 { get; set; }
    }
}