using System.ComponentModel.DataAnnotations;

using static MatchMateInfrastructure.ErrorMessages;
using static MatchMateInfrastructure.DataConstants.OfferConstants;
using MatchMateCore.Dtos.UsersViewModels;

namespace MatchMateCore.Dtos.OfferViewModels
{
    public class OfferPostFormModel
    {

        [Required(ErrorMessage =RequiredErrorMessage)]
        [StringLength(MaxTitleLength, MinimumLength =MinTitleLength,
            ErrorMessage =StringLengthErrorMessage)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(MaxDescriptionLength, MinimumLength = MinDescriptionLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(MaxPlaceLength, MinimumLength = MinPlaceLength,
             ErrorMessage = StringLengthErrorMessage)]
        public string Place { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Time { get; set; } = string.Empty;
        public UserOfferModel ReceiverData { get; set; } = null!;

    }
}
