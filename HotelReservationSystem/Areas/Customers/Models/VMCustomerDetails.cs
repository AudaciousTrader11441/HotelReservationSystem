using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelReservationSystem.Areas.Customers.Models
{
    public class VMCustomerDetails
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Contact { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int Pincode { get; set; }
    }
    public class VMECustomerDetails
    {
        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }
        public long c_contact { get; set; }
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Enter date of Birth.")]
        public System.DateTime Dob { get; set; }
        //[Required]
        public string state { get; set; }
        // [Required]
        public string city { get; set; }
        // [Required]
        public int pincode { get; set; }
    }

}