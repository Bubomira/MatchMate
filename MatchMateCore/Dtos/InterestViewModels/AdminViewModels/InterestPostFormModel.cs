using System.ComponentModel.DataAnnotations;
using static MatchMateInfrastructure.DataConstants.InterestDataConstants;
using static MatchMateInfrastructure.ErrorMessages;


namespace MatchMateCore.Dtos.InterestViewModels.AdminViewModels
{
    public class InterestPostFormModel
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Name { get; set; } = string.Empty;
    }
}
