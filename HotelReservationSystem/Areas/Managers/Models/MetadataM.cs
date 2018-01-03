using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelReservationSystem.Areas.Managers.Models
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
    public class VMEmanager
    {
        [Display(AutoGenerateField = false)]
        
        [Key]
        public string id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }
        [Required]
        [RegularExpression(@"^[789]\d{9}$", ErrorMessage = "Mobile number not vaild")]
        [Display(Name = "Mobile number")]
        public long m_contact { get; set; }

    }
    public class AnationsManager
    {
        [Display(AutoGenerateField = false)]

        [Key]
        public string id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{6,20})", ErrorMessage = "Password need to contain 1 upper case ,1 special @#$# and one digit")]

        [Display(Name = "Password")]
        public string password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("password", ErrorMessage = "The password and confirmation password do not match.")]
        public string confirmpassword { get; set; }
        [Required]
        [RegularExpression(@"^[789]\d{9}$", ErrorMessage = "Mobile number not vaild")]
        [Display(Name = "Mobile number")]
        public long m_contact { get; set; }
        [Required]
        [EmailAddress]
        [Duplicateemail(ErrorMessage = "Email already used")]
        [Display(Name = "Email")]
       public string m_email { get; set; }


    }
    [MetadataType(typeof(AnationsManager))]
    public partial class Manager { }
}