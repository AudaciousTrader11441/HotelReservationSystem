using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity;
namespace HotelReservationSystem.Areas.Managers.Models
{
    public partial class VMhotels
    {
        [Display(AutoGenerateField = false)]
        [HiddenInput(DisplayValue = false)]
        public string id { get; set; }
        [Required]
        [Display(Name = "Hotel Name")]
        public string name { get; set; }
        [Required]
        [Display(Name = "Hotel Description")]
        public string hotel_description { get; set; }
        [Required]
        [Display(Name = "No of AC Rooms")]
        public int no_of_ac_rooms { get; set; }
        [Required]
        [Display(Name = "No of NON-AC Rooms")]
        public int no_of_non_ac_rooms { get; set; }

        [Required]
        [Display(Name = "Cost of Adult in AC Rooms")]
        public int Cost_ac_adult { get; set; }

        [Required]
        [Display(Name = "Cost of Adult in Non AC Rooms")]
        public int Cost_non_ac_adult { get ; set; }

        [Required]
        [Display(Name = "Cost of Child in AC Rooms")]
        public int Cost_ac_child { get; set; }

        [Required]
        [Display(Name = "Cost of Child in Non AC Rooms")]
        public int Cost_non_ac_child { get; set; }

        [Required]
        [Display(Name = "State")]
        public string state { get; set; }
        [Required]
        [Display(Name = "City")]
        public string city { get; set; }
        [Required]
        [Display(Name = "Pincode")]
        public int pincode { get; set; }
    }
    [MetadataType(typeof(VMhotels))]
    public partial class Hotel{}

}