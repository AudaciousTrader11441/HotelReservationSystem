using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelReservationSystem.Areas.Customers.Models
{
    public class VMBookingConfirm
    {
        public string Customerid { get; set; }
        public string Hotelid { get; set; }
        public String Tid { get; set; }
        public string Bid { get; set; }
        
    }
}