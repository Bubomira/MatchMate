using System.ComponentModel.DataAnnotations;

using static MatchMateInfrastructure.DataConstants.ApplicationUserDataConstants;
using static MatchMateInfrastructure.ErrorMessages;

namespace MatchMateCore.Dtos.UsersViewModels
{
    public class UserBioModel
    {

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(MaxBioLength, MinimumLength = MinBioLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Bio { get; set; } = string.Empty;
        public bool HasBio { get; set; } = false;
    }
}
