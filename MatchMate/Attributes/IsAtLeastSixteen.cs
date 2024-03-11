using System.ComponentModel.DataAnnotations;

namespace MatchMate.Attributes
{
    public class IsAtLeastSixteen : ValidationAttribute
    {
        private readonly DateTime date;
        public IsAtLeastSixteen()
        {
            date = DateTime.Now;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {

                if (((DateTime)value).AddYears(16) < date)
                {
                    return ValidationResult.Success;
                }

            }
            return new ValidationResult("You have to be at least 16 to use this app!");
        }
    }
}
