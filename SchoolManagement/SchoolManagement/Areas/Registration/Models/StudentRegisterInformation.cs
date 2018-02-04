using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolManagement.Areas.Registration.Models
{
    public class StudentRegisterInformation
    {
        public int Contact_PersonID { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = " First Name")]
        public string first_name { get; set; }

        [Display(Name = " Last Name")]
        public string last_name { get; set; }

        [Display(Name = " Middle Name")]
        public string middle_name { get; set; }

        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        // [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        [Display(Name = " Contact Number(1)")]
        public string contact_number1 { get; set; }

        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        // [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        [Display(Name = " Contact Number(2)")]
        public string contact_number2 { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Email is not valid")]
        [Display(Name = "Email")]
        public string contact_email { get; set; }

        [Display(Name = "Relation with Student")]
        public string RelationWith_Student { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }




        //# Student Region
        public int StudentID { get; set; }

        [Display(Name = " First Name")]
        public string first_names { get; set; }
        [Display(Name = " Middle Name")]
        public string middle_names { get; set; }

        [Display(Name = " Last Name")]
        public string last_names { get; set; }

        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? birthdate { get; set; }

        [Display(Name = "Contact Number")]
        public string contact { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Email is not valid")]
        [Display(Name = "Email")]
        public string contact_emails { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Adhar Number")]
        public int AdharNumber { get; set; }

        [Display(Name = "Local Address")]
        public string LocalAddress { get; set; }

        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }


    }
}