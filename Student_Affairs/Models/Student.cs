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
        public string Name { get; set; }
        [Required]
        [StringLength(80, MinimumLength = 3)]
        public string Address { get; set; }
        [Required]
        [BirthDateValidation]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public int ClassID { get; set; }

        public Class Class { get; set; }
        public ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}
