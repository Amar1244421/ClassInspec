//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolManagement
{
    using System;
    using System.Collections.Generic;

    using System.ComponentModel.DataAnnotations;

    public partial class Instructor
    {
        public int InstructorID { get; set; }
        [Display(Name = " First Name")]
        public string first_name { get; set; }
        [Display(Name = "Last Name")]
        public string last_name { get; set; }
        [Display(Name = "Title")]
        public string title { get; set; }
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? birthdate { get; set; }

        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        // [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        [Display(Name = "Contact Number")]
        public string contact { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "Email is not valid")]
        [Display(Name = "Email")]
        public string contact_email { get; set; }
    }
}