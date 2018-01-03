using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelReservationSystem.Areas.Customers.Models
{
    [AttributeUsage(AttributeTargets.Property |
  AttributeTargets.Field, AllowMultiple = false)]
    sealed public class DuplicateemailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (Business.BussLogic.EmailVerify(value.ToString()) == 0)
                return true;
            else
                return false;


            
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(name);
        }

    }
    public class VMECustomer
    {
        [Display(AutoGenerateField = false)]
        [HiddenInput(DisplayValue = false)]
        [Key]
        public string id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }
        [Required]
        [RegularExpression(@"^[789]\d{9}$", ErrorMessage = "Mobile number not vaild")]
        [Display(Name = "Mobile number")]
        public long c_contact { get; set; }
        [Required]
        [DataType(DataType.Date, ErrorMessage = "Enter date of Birth.")]
        public System.DateTime Dob { get; set; }
        [Required]
        public string state { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public int pincode { get; set; }
    }

    public class AnationsCustomer
    {

        [Display(AutoGenerateField = false)]
        [HiddenInput(DisplayValue = false)]
        public string id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{6,20})",ErrorMessage ="Password need to contain 1 upper case ,1 special @#$# and one digit")]
        public string password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [System.ComponentModel.DataAnnotations.Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string confirmpassword { get; set; }
        [Required]
        [RegularExpression(@"^[789]\d{9}$",ErrorMessage ="Mobile number not vaild")]
        [Display(Name = "Mobile number")]
        public long c_contact { get; set; }
        [Required]
        [EmailAddress]
        [Duplicateemail(ErrorMessage ="Email already used")]
        [Display(Name = "Email")]
        public string c_email { get; set; }
       [Required]
       [DataType(DataType.Date,ErrorMessage ="Enter date of Birth.") ]
        public System.DateTime Dob { get; set; }
        [Required]
        public string state { get; set; }
       [Required]
        public string city { get; set; }
       [Required]
        public int pincode { get; set; }


    }
    [MetadataType(typeof(AnationsCustomer))]
    public partial class Customer { }
}