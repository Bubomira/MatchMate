using MatchMateInfrastructure.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMateInfrastructure.Models
{
    [Comment("A friendship between two users")]
    public class Friendship
    {
        [Key]
        [Required]
        [Comment("The unique identifyer of a friendship")]
        public int Id { get; set; }

        [Required]
        [Comment("The status of a friendship request, can be rejected, pending, active")]
        public FriendshipStatus Status { get; set; } = FriendshipStatus.Pending;

        [Required]
        [Comment("The id of the person who sends a friendship request")]
        public string SenderId { get; set; } = null!;

        [ForeignKey(nameof(SenderId))]
        public ApplicationUser Sender { get; set; } = null!;

        [Required]
        [Comment("The person who receives a friendship request")]
        public string ReceiverId { get; set; } = null!;

        [ForeignKey(nameof(ReceiverId))]
        public ApplicationUser Receiver { get; set; } = null!;
    }
}
