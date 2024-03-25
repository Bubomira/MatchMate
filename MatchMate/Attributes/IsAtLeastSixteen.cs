using System.ComponentModel.DataAnnotations;
using System.Globalization;
using static MatchMateInfrastructure.DataConstants;

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
                var parsedValue = DateTime.ParseExact(value.ToString(), BirthdateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);

                if (parsedValue.AddYears(16) < date)
                {
                    return ValidationResult.Success;
                }

            }
            return new ValidationResult("You have to be at least 16 to use this app!");
        }
    }
}
