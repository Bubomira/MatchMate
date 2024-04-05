using System.ComponentModel.DataAnnotations;

using static MatchMateInfrastructure.DataConstants.MessageConstants;
using static MatchMateInfrastructure.ErrorMessages;

namespace MatchMateCore.Dtos.MessageViewModels
{
    public class MessagePostFormModel
    {
        public string ReceiverId { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(MaxContentLength, MinimumLength = MinContentLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Content { get; set; } = string.Empty;
    }
}
