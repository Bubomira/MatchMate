using MatchMateInfrastructure.Enums;
using System.ComponentModel.DataAnnotations;

using static MatchMateInfrastructure.DataConstants.ReportsConstants;
using static MatchMateInfrastructure.ErrorMessages;

namespace MatchMateCore.Dtos.OfferViewModels
{
    public class OfferReportPostModel
    {
        [Required]
        public int Id { get; set; }
        [StringLength(CommentMaxLength, ErrorMessage = StringLengthErrorMessage)]
        public string? Comment { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public ReasonForReport ReasonForReport { get; set; } = ReasonForReport.Other;
    }
}
