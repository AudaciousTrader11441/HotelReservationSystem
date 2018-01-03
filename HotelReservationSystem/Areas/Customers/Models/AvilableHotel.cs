using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelReservationSystem.Areas.Customers.Models
{
    public class AvilableHotel
    {
        public string HotelID { get; set; }
        public string HotelName { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int AcRooms { get; set; }
        public int NonAcRooms { get; set; }

    }
}