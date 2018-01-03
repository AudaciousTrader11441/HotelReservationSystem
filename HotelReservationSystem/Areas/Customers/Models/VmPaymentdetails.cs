using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelReservationSystem.Areas.Customers.Models
{
    public class VmPaymentdetails
    {
        [Required]
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }
        [Required]
        [Display(Name = "Card Type")]
        public string CardType { get; set; }
        [Required]
        [Display(Name = "Card No")]
        public int CardNo { get; set; }
        [Required]
        [Display(Name = "Name on Card")]
        public string CardName { get; set; }
        [Required]
        [Display(Name = "Account No")]
        public int AccountNo { get; set; }
        [Required]
        [Display(Name = "Pin")]
        public int Pin { get; set; }
        [Required]
        [Display(Name = "Expire Date select 01-01-2021")]
        [DataType(DataType.Date, ErrorMessage = "Enter date of Check out.")]
        public DateTime ExpireDate { get; set; }
        [Required]
        [Display(Name = "CVV")]
        public int CVV { get; set; }
        [Required]
        [Display(Name = "Amount ")]
        public int totalcost { get; set; }

    }
}