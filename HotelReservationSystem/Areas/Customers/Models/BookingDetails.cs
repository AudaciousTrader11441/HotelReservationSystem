using Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelReservationSystem.Areas.Customers.Models
{
    public class AnationsBooking
    {
        [Display(AutoGenerateField = false)]
        [HiddenInput(DisplayValue = false)]
        [Key]
        public string Booking_id { get; set; }
        [Required]
        [Display(Name = "Customer ID")]
        public string Customer_id { get; set; }
        [Required]
        [Display(Name = "Hotel ID")]
        public string Hotel_id { get; set; }
        [Required]
        [Display(Name = "Booking Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Type")]
        public int Type { get; set; }
        [Required]
        [Display(Name = "Check In")]
        [DataType(DataType.Date, ErrorMessage = "Enter date of Check in.")]
        public DateTime Date_checkin { get; set; }

        [Required]
        [Display(Name = "Check Out")]
        [DataType(DataType.Date, ErrorMessage = "Enter date of Check out.")]
        public DateTime Date_checkout { get; set; }
        [Required]
        [Display(Name = "No of adults")]
        public int no_adult { get; set; }
        [Required]
        [Display(Name = "No of Child")]
        public int no_child { get; set; }
        [Required]
        [Display(Name = "Total Cost")]
        public int TotalCost { get; set; }
    }
    [MetadataType(typeof(AnationsBooking))]
    public partial class Bookdetails : Booking
    { }

    public class AnationsBookingWid
    {
        [Required]
        public string Customer_id { get; set; }
        public string Hotel_id { get; set; }
        public string Booking_id { get; set; }

        [Required]
        [DataType(DataType.Date, ErrorMessage = "Enter date of Birth.")]
        public DateTime Date_checkin { get; set; }
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Enter date of Birth.")]
        public DateTime Date_checkout { get; set; }
        public int no_adult { get; set; }
        public int no_child { get; set; }
       
    }
    
}