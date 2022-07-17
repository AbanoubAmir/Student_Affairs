using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Student_Affairs.Models
{
    public class Class
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Name*")]
        public string Name { get; set; }

        public string Description { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
