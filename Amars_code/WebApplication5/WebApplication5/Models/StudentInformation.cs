using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class StudentInformation_Result
    {
        public IEnumerable<StudentInformation_Result> StudentListCollction { get; set; }

        //Contact Person Region
        public int Contact_PersonID { get; set; }

        [Required(ErrorMessage ="Please enter the First name")]
        [Display(Name=" First Name")]
        public string first_name { get; set; }

        [Display(Name = " Last Name")]
        public string last_name { get; set; }

        [Display(Name = " Middle Name")]
        public string middle_name { get; set;}

        [Display(Name = " Contact Number(1)")]
        public string contact_number1 { get; set; }

        [Display(Name = " Contact Number(2)")]
        public string contact_number2 { get; set; }

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

        [Display(Name = "Birth Date")]
        public Nullable<System.DateTime> birthdate { get; set; }

        [Display(Name = "Contact Number")]
        public string contact { get; set; }

        [Display(Name = "Email")]
        public string contact_emails { get; set; }

        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Adhar Number")]
        public Int64 AdharNumber { get; set; }

        [Display(Name = "Local Address")]
        public string LocalAddress { get; set; }

        [Display(Name = "Permanent Address")]
        public string PermanentAddress { get; set; }

        [Display(Name = "Class Type Id")]
        public string ClassTypeId { get; set; }


    }
}