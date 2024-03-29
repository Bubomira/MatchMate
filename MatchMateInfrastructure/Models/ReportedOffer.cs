using MatchMateInfrastructure.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static MatchMateInfrastructure.DataConstants.ReportsConstants;

namespace MatchMateInfrastructure.Models
{
    public class ReportedOffer
    {
        [Key]
        [Required]
        [Comment("The unique identifyer of the offer report")]
        public int Id { get; set; }

        [Required]
        [Comment("The reason for submitting the report the report")]
        public ReasonForReport ReasonForRepport { get; set; } = ReasonForReport.Other;

        [MaxLength(CommentMaxLength)]
        [Comment("Aditional explanations for the offer report")]
        public string? Comment { get; set; }

        [Required]
        [Comment("Shows if admin finds this report a strike towards the suggester resume")]
        public bool IsReasonable { get; set; } = true;

        [Required]
        [Comment("The id of the reported offer")]
        public int OfferId { get; set; }

        [ForeignKey(nameof(OfferId))]
        public Offer Offer { get; set; } = null!;

    }
}
