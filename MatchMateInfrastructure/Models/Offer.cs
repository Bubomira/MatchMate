using MatchMateInfrastructure.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MatchMateInfrastructure.DataConstants.OfferConstants;

namespace MatchMateInfrastructure.Models
{
    [Comment("Offer for a meetup, which will be send by one user to another")]
    public class Offer
    {
        [Key]
        [Required]
        [Comment("The unique identifyer for an offer")]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxTitleLength)]
        [Comment("The title of the offer")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Comment("The status of the offer, can be rejected, pending, cancelled, accepted")]
        public OfferStatus Status { get; set; } = OfferStatus.Pending;

        [Required]
        [MaxLength(MaxDescriptionLength)]
        [Comment("A brief desription of the activity")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(MaxPlaceLength)]
        [Comment("The location with words of the meeting")]
        public string Place { get; set; } = string.Empty;

        [Required]
        [Comment("The date of the offer")]
        public DateTime Time { get; set; }

        [Required]
        [Comment("The id of the user who made the offer")]
        public string SuggestingUserId { get; set; } = null!;

        [ForeignKey(nameof(SuggestingUserId))]
        public ApplicationUser SuggestingUser { get; set; } = null!;

        [Required]
        [Comment("The id of the user who is offered the offer")]
        public string ReceivingUserId { get; set; } = null!;

        [ForeignKey(nameof(ReceivingUserId))]
        public ApplicationUser ReceivingUser { get; set; } = null!;
        public ReportedOffer? ReportedOffer { get; set; }

    }
}
