//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication5
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Instructor
    {
        public int InstructorID { get; set; }
        [Display(Name = " First Name")]
        public string first_name { get; set; }
        [Display(Name ="Last Name")]
        public string last_name { get; set; }
        [Display(Name ="Title")]
        public string title { get; set; }
        [Display(Name ="Date Of Birth")]
        public Nullable<System.DateTime> birthdate { get; set;}
        [Display(Name ="Contact Number")]
        public string contact { get; set; }
        [Display(Name ="Email")]
        public string contact_email { get; set; }
    }
}
