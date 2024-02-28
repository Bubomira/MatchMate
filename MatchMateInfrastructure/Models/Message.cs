using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MatchMateInfrastructure.DataConstants.MessageConstants;

namespace MatchMateInfrastructure.Models
{
    [Comment("Message send by user via the chat")]
    public class Message
    {
        [Key]
        [Required]
        [Comment("The unique identifyer for a message")]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxContentLength)]
        [Comment("The core of the message")]
        public string Content { get; set; } = string.Empty;

        [Required]
        [Comment("The id of the person who received the message")]
        public string ReceiverId { get; set; } = string.Empty;

        [ForeignKey(nameof(ReceiverId))]
        public ApplicationUser Receiver { get; set; } = null!;

        [Required]
        [Comment("The id of the person who sended the message")]
        public string SenderId { get; set; } = string.Empty;

        [ForeignKey(nameof(SenderId))]
        public ApplicationUser Sender { get; set; }
    }
}
