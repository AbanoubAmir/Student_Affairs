using System;
using System.ComponentModel.DataAnnotations;

namespace Student_Affairs.Models.Helpers
{
    public class BirthDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // your validation logic
            if (DateTime.Today.AddYears(-100).Date.CompareTo(value) <= 0 && DateTime.Today.CompareTo(value) >= 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Birthdate is not possible.");
            }
        }
    }
}
