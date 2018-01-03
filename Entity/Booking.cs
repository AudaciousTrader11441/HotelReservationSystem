//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Booking
    {
        public string Booking_id { get; set; }
        public string Customer_id { get; set; }
        public string Hotel_id { get; set; }
        public Nullable<int> No_Of_AC_Rooms { get; set; }
        public int No_Of_Non_AC_Rooms { get; set; }
        public string T_id { get; set; }
        public Nullable<System.DateTime> Date_booking { get; set; }
        public Nullable<System.DateTime> Date_checkin { get; set; }
        public Nullable<System.DateTime> Date_checkout { get; set; }
        public int no_adult { get; set; }
        public Nullable<int> no_child { get; set; }
        public Nullable<int> no_nights { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
