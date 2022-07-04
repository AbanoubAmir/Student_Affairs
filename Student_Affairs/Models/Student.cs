using Student_Affairs.Models.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student_Affairs.Models
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Name*")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Address*")]
        [StringLength(80, MinimumLength = 3)]
        public string Address { get; set; }
        [Required]
        [BirthDateValidation]
        [Display(Name = "DOB*")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email*")]
        public string Email { get; set; }
        [Display(Name = "Class Name*")]
        public int ClassID { get; set; }

        public Class Class { get; set; }
        public ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}
