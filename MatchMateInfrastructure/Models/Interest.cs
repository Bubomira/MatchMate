using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using static MatchMateInfrastructure.DataConstants.InterestDataConstants;

namespace MatchMateInfrastructure.Models
{
    public class Interest
    {
        [Key]
        [Required]
        [Comment("The unique identifyer for an interest")]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxNameLength)]
        [Comment("A name for the interest, will hold the meaning")]
        public string Name { get; set; } = string.Empty;

        public IList<UserInterest> UserInterest { get; set; } = new List<UserInterest>();

    }
}
