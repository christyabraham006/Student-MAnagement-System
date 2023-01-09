using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Student_Management_System.Models
{
    public class StudentModel
    {
        public int ID{ get; set; }
        [Required(ErrorMessage = "Please enter name"), MaxLength(30)]
        [RegularExpression(@"[A-Za-z ]{1,32}", ErrorMessage = "Not a Valid Name.")]
        public string Name { get; set; }
        [Display(Name="Admission Number")]
        //[Required(ErrorMessage = "Please enter Admission number")]
        public string AdmissionNo { get; set; }
        [Display(Name = "Date of birth")]
        [Required(ErrorMessage = "Please enter Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DOB { get; set; }
        [Display(Name = "Father's Name")]
        [Required(ErrorMessage = "Please enter Father's Name")]
        public string FatherName { get; set; }
        [Required(ErrorMessage = "Please enter Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter the class")]
        public int Standard { get; set; }
        [Required(ErrorMessage = "Please enter the Contact number")]
        [RegularExpression(@"[0-9]{10}", ErrorMessage = "Not a Valid Phone No.")]
        public string Contact { get; set; }



    }
}