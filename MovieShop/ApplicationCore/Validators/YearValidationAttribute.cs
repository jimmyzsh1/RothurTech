using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Validators
{
    public class YearValidationAttribute : ValidationAttribute
    {
        public int MinYear;
        public int MaxYear;

        public YearValidationAttribute(int min, int max)
        {
            MinYear = min;
            MaxYear = max;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var userEnteredYear = ((DateTime)value).Year;
            if (userEnteredYear >= MinYear && userEnteredYear <= MaxYear)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Please Enter Correct Year");
            }
        }
    }
}
