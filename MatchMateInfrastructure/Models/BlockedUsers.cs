
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchMateInfrastructure.Models
{
    public class BlockedUsers
    {
        [Key]
        [Comment("The id of the set, the composite key would have been too large.")]
        public int Id { get; set; }
        [Required]
        [Comment("The id of the blocked user")]
        public string BlockedUserId { get; set; } = string.Empty;

        [ForeignKey(nameof(BlockedUserId))]
        public ApplicationUser BlockedUser { get; set; } = null!;

        [Required]
        [Comment("The id of the user who has blocked the other")]
        public string BlockerUserId { get; set; } = string.Empty;

        [ForeignKey(nameof(BlockerUserId))]
        public ApplicationUser BlockerUser { get; set; } = null!;
    }
}
